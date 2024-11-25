using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.UpdateMainInfo.RequestModels;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    public UpdateMainInfoHandler(
        IVolunteerRepository repository,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _repository.GetByIdAsync(request.Id);

        _logger.LogInformation("Volunteer was updated with id: {0}", request.Id);

        return (Guid)request.Id;
    }
}