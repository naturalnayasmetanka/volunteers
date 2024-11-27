using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.UpdateRequisites.RequestModels;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.UpdateRequisites;

public class UpdateRequisitesHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateRequisitesHandler> _logger;

    public UpdateRequisitesHandler(
        IVolunteerRepository repository,
        ILogger<UpdateRequisitesHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        UpdateRequisiteRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(request.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(request.Id));
            return _errors;
        }

        List<VolunteerRequisite> newRequisites = new List<VolunteerRequisite>();
        request.RequisitesDTO.RequisiteList.ForEach(x => newRequisites.Add(VolunteerRequisite.Create(x.Title, x.Description).Value));

        volunteer.UpdateRequisites(newRequisites);

        await _repository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("id: {0} Volunteer requisites info was updated", request.Id);

        return (Guid)request.Id;
    }
}