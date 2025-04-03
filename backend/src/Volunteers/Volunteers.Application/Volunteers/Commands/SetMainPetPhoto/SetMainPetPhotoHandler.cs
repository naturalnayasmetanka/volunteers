using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteers.Application.Volunteers.Commands.UpdatePet;

namespace Volunteers.Application.Volunteers.Commands.SetMainPetPhoto;

public class SetMainPetPhotoHandler : ICommandHandler<Guid, SetMainPetPhotoCommand>
{
    private readonly ILogger<UpdatePetHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;

    public SetMainPetPhotoHandler(
        ILogger<UpdatePetHandler> logger,
        IVolunteerRepository volunteerRepository)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Handle(
        SetMainPetPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteer = await _volunteerRepository.GetByIdAsync(volunteerId, cancellationToken);

        if (volunteer is null)
        {
            _logger.LogError("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(SetMainPetPhotoHandler));

            return Errors.General.NotFound(command.VolunteerId);
        }

        var setMainPhotoResult = volunteer.SetMainPetPhoto(path: command.FilePath, petId: PetId.Create(command.PetId));

        if (setMainPhotoResult.IsFailure)
        {
            _logger.LogError("Photo {0} was not set as main into {1}", command.VolunteerId, nameof(SetMainPetPhotoHandler));

            return setMainPhotoResult.Error;
        }

        await _volunteerRepository.SaveAsync(cancellationToken);

        return command.PetId;
    }
}
