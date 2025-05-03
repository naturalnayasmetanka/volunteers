using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;
using Shared.Kernel.Ids;
using Volunteers.Application.Volunteers.Commands.Delete.Commands;

namespace Volunteers.Application.Volunteers.Commands.Delete;

public class SoftDeleteVolunteerHandler : ICommandHandler<Guid, SoftDeleteVolunteerCommand>
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

    public SoftDeleteVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<SoftDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        SoftDeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(command.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _logger.LogError("Volunteer {0} was not found into {1}", id.Value, nameof(SoftDeleteVolunteerHandler));

            return Errors.General.NotFound(command.Id);
        }

        volunteer.SoftDelete();
        await _repository.SaveAsync();

        _logger.LogInformation("Volunteer {0} was deleted(soft delete) into {1}", command.Id, nameof(SoftDeleteVolunteerHandler));

        return command.Id;
    }
}
