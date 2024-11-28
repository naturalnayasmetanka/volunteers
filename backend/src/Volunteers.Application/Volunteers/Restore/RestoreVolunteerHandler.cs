using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.Delete;
using Volunteers.Application.Volunteers.Restore.RequestModels;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.Restore;

public class RestoreVolunteerHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;

    public RestoreVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<HardDeleteVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        RestoreRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(request.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(request.Id));
            return _errors;
        }

        volunteer.Restore();

        await _repository.SaveAsync();

        _logger.LogInformation("Volunteer was restored with id: {0}", request.Id);

        return request.Id;
    }
}