using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers;
using Volunteers.Application.Handlers.Volunteers.Commands.Delete.Commands;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Handlers.Volunteers.Commands.Delete;

public class HardDeleteVolunteerHandler : ICommandHandler<Guid, DeleteCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;

    public HardDeleteVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<HardDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteCommand command,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(command.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _logger.LogError("Volunteer {0} was not found into {1}", id.Value, nameof(HardDeleteVolunteerHandler));

            return Errors.General.NotFound(command.Id);
        }

        await _repository.DeleteAsync(volunteer);

        _logger.LogInformation("Volunteer {0} was deleted(hard delete) into {1}", command.Id, nameof(HardDeleteVolunteerHandler));

        return command.Id;
    }
}
