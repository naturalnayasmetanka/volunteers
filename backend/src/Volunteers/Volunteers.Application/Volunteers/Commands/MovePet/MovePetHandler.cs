using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;
using Shared.Kernel.Ids;
using Volunteers.Application.Volunteers.Commands.MovePet.Commands;
using Volunteers.Domain.Pets.ValueObjects;

namespace Volunteers.Application.Volunteers.Commands.MovePet;

public class MovePetHandler : ICommandHandler<Guid, MovePetCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<MovePetHandler> _logger;

    public MovePetHandler(
        IVolunteerRepository repository,
        ILogger<MovePetHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
       MovePetCommand command,
       CancellationToken cancellationToken = default)
    {
        var volunteer = await _repository.GetByIdAsync(VolunteerId.Create(command.VolunteerId));

        if (volunteer is null)
        {
            _logger.LogError("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(MovePetHandler));

            return Errors.General.NotFound(command.VolunteerId);
        }

        var movePetResult = volunteer.MovePetPosition(PetId.Create(command.PetId), Position.Create(command.NewPosition).Value);

        if (movePetResult.IsFailure)
        {
            _logger.LogError("Pet {0} was not moved in the volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(MovePetHandler));

            return movePetResult.Error;
        }

        await _repository.SaveAsync(cancellationToken);

        _logger.LogInformation("Pet {0} was moved in the volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(MovePetHandler));

        return (Guid)volunteer.Id;
    }
}
