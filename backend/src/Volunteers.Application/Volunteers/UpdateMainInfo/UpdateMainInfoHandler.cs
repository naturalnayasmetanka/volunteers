using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.UpdateMainInfo.Commands;
using Volunteers.Application.Volunteers.UpdateMainInfo.DTO;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;
    private readonly IValidator<UpdateMainInfoDto> _validator;

    public UpdateMainInfoHandler(
        IVolunteerRepository repository,
        ILogger<UpdateMainInfoHandler> logger,
        IValidator<UpdateMainInfoDto> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command.MainInfoDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.Errors.ForEach(error => _errors.Add(Error.Validation(error.ErrorMessage, error.ErrorCode)));
            _logger.LogError("Validation is failed into {0}", nameof(UpdateMainInfoHandler));

            return _errors;
        }

        var id = VolunteerId.Create(command.VolunteerId);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(command.VolunteerId));
            _logger.LogError("Volunteer {0} was not found into {1}", id.Value, nameof(UpdateMainInfoHandler));

            return _errors;
        }

        volunteer.UpdateMainInfo(
            name: Name.Create(command.MainInfoDto.Name).Value,
            email: Email.Create(command.MainInfoDto.Email).Value,
            experienceInYears: ExperienceInYears.Create(command.MainInfoDto.ExperienceInYears).Value,
            phoneNumber: PhoneNumber.Create(command.MainInfoDto.PhoneNumber).Value);

        await _repository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("Volunteer`s {0} main info was updated into {1}", id, nameof(UpdateMainInfoHandler));

        return (Guid)command.VolunteerId;
    }
}