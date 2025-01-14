using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Providers;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.AddPet;
using Volunteers.Application.Volunteers.DeletePetPhoto.Commands;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.DeletePetPhoto;

public class DeletePetPhotoHandler
{
    private readonly IMinIoProvider _minIoProvider;
    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;

    public DeletePetPhotoHandler(
        IMinIoProvider minIoProvider,
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository)
    {
        _minIoProvider = minIoProvider;
        _logger = logger;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<string, Error>> Handle(
        DeletePetPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _volunteerRepository.GetByIdAsync(VolunteerId.Create(command.VolunteerId));

        if (volunteer is null)
            return Errors.General.NotFound(command.VolunteerId);

        var pet = volunteer.Pets.FirstOrDefault(x => x.Id == PetId.Create(command.PetId));

        if (pet is null)
            return Errors.General.NotFound(command.PetId);

        var actualPhoto = pet.PhotoDetails?.PetPhoto.Where(x => x.Path != command.FileData.FileName).ToList();

        if (actualPhoto is not null)
            pet.UpdatePhoto(actualPhoto);

        var result = await _minIoProvider.DeleteAsync(command.FileData, cancellationToken);

        await _volunteerRepository.UpdateAsync(volunteer, cancellationToken: cancellationToken);

        _logger.LogInformation("File was deleted: {0}", command.FileData.FileName);

        return command.FileData.FileName;
    }
}
