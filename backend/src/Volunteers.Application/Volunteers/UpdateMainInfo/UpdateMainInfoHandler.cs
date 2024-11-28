using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.UpdateMainInfo.RequestModels;
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
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = VolunteerId.Create(request.Id);
        var volunteer = await _repository.GetByIdAsync(id, cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(request.Id));
            return _errors;
        }

        var name = Name.Create(request.MainInfoDto.Name).Value;
        var email = Email.Create(request.MainInfoDto.Email).Value;
        var experienceInYears = ExperienceInYears.Create(request.MainInfoDto.ExperienceInYears).Value;
        var phoneNumber = PhoneNumber.Create(request.MainInfoDto.PhoneNumber).Value;

        volunteer.UpdateMainInfo(
            name: name,
            email: email,
            experienceInYears: experienceInYears,
            phoneNumber: phoneNumber);

        await _repository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("id: {0} Volunteer main info was updated", id);

        return (Guid)request.Id;
    }
}