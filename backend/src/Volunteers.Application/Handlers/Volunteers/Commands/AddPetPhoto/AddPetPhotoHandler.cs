using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Database;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Volunteers;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPet;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPetPhoto.Commands;
using Volunteers.Application.MessageQueues;
using Volunteers.Application.Providers;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Handlers.Volunteers.Commands.AddPetPhoto;

public class AddPetPhotoHandler : ICommandHandler<Guid, AddPetPhotoCommand>
{
    private readonly IMinIoProvider _minIoProvider;
    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<List<FileDTO>> _messageQueue;

    public AddPetPhotoHandler(
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository,
        IMinIoProvider minIoProvider,
        IUnitOfWork unitOfWork,
        IMessageQueue<List<FileDTO>> messageQueue)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _minIoProvider = minIoProvider;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
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

            var volunteer = await _volunteerRepository.GetByIdAsync(volunteerId);

            if (volunteer is null)
            {
                _logger.LogError("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(AddPetPhotoHandler));

                return Errors.General.NotFound(command.VolunteerId);
            }

            var pet = volunteer.Pets.Where(x => x.Id.Value == petId.Value).FirstOrDefault();

            if (pet is null)
            {
                _logger.LogError("Pet {0} was not found in the volunteer {1} into {2}",
                    command.PetId,
                    command.VolunteerId,
                    nameof(AddPetPhotoHandler));

                return Errors.General.NotFound(command.PetId);
            }

            command.Photo.ForEach(photo =>
            {
                pet.AddPhoto(PetPhoto.Create(photo.FileName).Value);

                _logger.LogInformation("Pet {0} was added to volunteer {1} into {2}", petId, command.VolunteerId, nameof(AddPetPhotoHandler));
            });

            _volunteerRepository.Attach(volunteer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var urlResult = await _minIoProvider.UploadAsync(command.Photo, cancellationToken);

            if (urlResult.IsFailure)
            {
                await _messageQueue.WriteAsync(urlResult.Value, cancellationToken);

                throw new Exception(urlResult.Error.ToString());
            }

            transaction.Commit();

            return (Guid)volunteer.Id;
        }
        catch (Exception ex)
        {
            transaction.Rollback();

            _logger.LogError(ex.Message);

            return Error.Failure(ex.Message, "add.photo");
        }
    }
}
