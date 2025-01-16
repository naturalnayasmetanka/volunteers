using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Database;
using Volunteers.Application.Providers;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.AddPet;
using Volunteers.Application.Volunteers.AddPetPhoto.Commands;
using Volunteers.Domain.PetManagment.Pet.Entities;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;
using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;

namespace Volunteers.Application.Volunteers.AddPetPhoto;

public class AddPetPhotoHandler
{
    private readonly IMinIoProvider _minIoProvider;
    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPetPhotoHandler(
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository,
        IMinIoProvider minIoProvider,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _minIoProvider = minIoProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> Handle(
    AddPetPhotoCommand command,
    CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var petId = PetId.Create(command.PetId);

            VolunteerModel? volunteer = await _volunteerRepository.GetByIdAsync(volunteerId);
            Pet? pet = default;

            if (volunteer is not null)
            {
                pet = volunteer.Pets.Where(x => x.Id.Value == petId.Value).FirstOrDefault();

                if (pet is not null)
                {
                    command.Photo.ForEach(photo =>
                    {
                        pet.AddPhoto(PetPhoto.Create(photo.FileName).Value);

                        _logger.LogInformation("Pet with id {0} was added to volunteer with id: {1}", petId, command.VolunteerId);
                    });

                    _volunteerRepository.Attach(volunteer);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    var urlResult = await _minIoProvider.UploadAsync(command.Photo, cancellationToken);

                    if (urlResult.IsFailure)
                        return urlResult.Error.First();

                    transaction.Commit();

                    return (Guid)volunteer.Id;
                }
            }

            _logger.LogInformation("Volunteer was not found with id: {0}", command.VolunteerId);

            return Errors.General.NotFound(command.VolunteerId);
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            _logger.LogError(ex.Message);

            return Error.Failure(ex.Message, "upload.file");
        }

    }
}
