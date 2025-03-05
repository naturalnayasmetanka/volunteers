using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers;
using Volunteers.Application.Handlers.Volunteers.Commands.MovePet.Commands;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Handlers.Volunteers.Commands.MovePet;

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

        var pet = volunteer.Pets.FirstOrDefault(x => x.Id == command.PetId);

        if (pet is null)
        {
            _logger.LogError("Pet {0} was not found in the volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(MovePetHandler));

            return Error.NotFound("Pet not found", "not.found");
        }

        volunteer.MovePetPosition(pet, Position.Create(command.NewPosition).Value);
        await _repository.SaveAsync(cancellationToken);

        _logger.LogInformation("Pet {0} was moved in the volunteer {1} into {2}", command.PetId, command.VolunteerId, nameof(MovePetHandler));

        return (Guid)volunteer.Id;
    }
}
