using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
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

    public AddPetPhotoHandler(
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository,
        IMinIoProvider minIoProvider)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _minIoProvider = minIoProvider;
    }

    public async Task<Result<VolunteerModel, Error>> Handle(
    AddPetPhotoCommand command,
    CancellationToken cancellationToken = default)
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
                List<PetPhoto> urls = [];
                var urlResult = await _minIoProvider.UploadAsync(command.Photo, cancellationToken);
                urlResult.Value.ForEach(url => urls.Add(PetPhoto.Create(url).Value));

                urls.ForEach(url =>
                {
                    pet.AddPhoto(url);

                    _logger.LogInformation("Pet with id {0} was added to volunteer with id: {1}", petId, command.VolunteerId);
                });

                return volunteer;
            }
        }

        _logger.LogInformation("Volunteer was not found with id: {0}", command.VolunteerId);

        return Errors.General.NotFound(command.VolunteerId);
    }
}
