using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePetStatus.Commands;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdatePetStatus;

public class UpdatePetStatusHandler : ICommandHandler<Guid, UpdatePetStatusCommand>
{
    private readonly ILogger<UpdatePetStatusHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdatePetStatusHandler(
        ILogger<UpdatePetStatusHandler> logger,
        IVolunteerRepository volunteerRepository)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdatePetStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteer = await _volunteerRepository.GetByIdAsync(volunteerId, cancellationToken);

        if (volunteer is null)
        {
            _logger.LogError("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(UpdatePetStatusHandler));

            return Errors.General.NotFound(command.VolunteerId);
        }

        var updateStatusResult = volunteer.UpdatePetStatus(newStatus: command.PetStatus, petId: command.PetId);

        if (updateStatusResult.IsFailure)
        {
            _logger.LogError("Pet {0} status was not updated to volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(UpdatePetStatusHandler));

            return updateStatusResult.Error;
        }

        _logger.LogInformation("Pet {0} status was updated to volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(UpdatePetStatusHandler));

        await _volunteerRepository.SaveAsync();

        return (Guid)updateStatusResult.Value.Id;
    }
}
