using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.Delete.Commands;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.Delete;

public class SoftDeleteVolunteerHandler
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

    public async Task<Result<Guid, List<Error>>> Handle(
        DeleteCommand command,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(command.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(command.Id));
            return _errors;
        }

        volunteer.SoftDelete();

        await _repository.SaveAsync();

        _logger.LogInformation("Volunteer was deleted with id (soft delete): {0}", command.Id);

        return command.Id;
    }
}