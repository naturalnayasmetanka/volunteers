using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites.DTO;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.Commands.UpdateRequisites;

public class UpdateRequisitesHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateRequisitesHandler> _logger;
    private readonly IValidator<UpdateRequisiteListDTO> _validator;

    public UpdateRequisitesHandler(
        IVolunteerRepository repository,
        ILogger<UpdateRequisitesHandler> logger,
        IValidator<UpdateRequisiteListDTO> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        UpdateRequisiteCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command.RequisitesDTO, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.Errors.ForEach(error => _errors.Add(Error.Validation(error.ErrorMessage, error.ErrorCode)));
            _logger.LogError("Validation is failed into {0}", nameof(UpdateRequisitesHandler));

            return _errors;
        }

        var id = VolunteerId.Create(command.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(command.Id));
            _logger.LogError("Volunteer {0} was not found into {1}", id.Value, nameof(UpdateRequisitesHandler));

            return _errors;
        }

        List<VolunteerRequisite> newRequisites = new List<VolunteerRequisite>();
        command.RequisitesDTO.RequisiteList.ForEach(x => newRequisites.Add(VolunteerRequisite.Create(x.Title, x.Description).Value));

        volunteer.UpdateRequisites(newRequisites);
        await _repository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("id: {0} Volunteer requisites info was updated into {1}", command.Id, nameof(UpdateRequisitesHandler));

        return command.Id;
    }
}