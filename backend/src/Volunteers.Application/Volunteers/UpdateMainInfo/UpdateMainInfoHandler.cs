using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.UpdateMainInfo.Commands;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

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
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(command.VolunteerId);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(command.VolunteerId));
            return _errors;
        }

        var name = Name.Create(command.MainInfoDto.Name).Value;
        var email = Email.Create(command.MainInfoDto.Email).Value;
        var experienceInYears = ExperienceInYears.Create(command.MainInfoDto.ExperienceInYears).Value;
        var phoneNumber = PhoneNumber.Create(command.MainInfoDto.PhoneNumber).Value;

        volunteer.UpdateMainInfo(
            name: name,
            email: email,
            experienceInYears: experienceInYears,
            phoneNumber: phoneNumber);

        await _repository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("id: {0} Volunteer main info was updated", id);

        return (Guid)command.VolunteerId;
    }
}