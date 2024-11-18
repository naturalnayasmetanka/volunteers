using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Volunteer.Models;
using VolunteerModel = Volunteers.Domain.Volunteer.Models.Volunteer;

namespace Volunteers.Application.Volunteer.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _repository;

    public CreateVolunteerHandler(IVolunteerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var name = Name.Create(request.VolunteerDto.Name);
        var email = Email.Create(request.VolunteerDto.Email);
        var experienceInYears = ExperienceInYears.Create(request.VolunteerDto.ExperienceInYears);
        var phoneNumber = PhoneNumber.Create(request.VolunteerDto.PhoneNumber);

        var volunteerResult = VolunteerModel.Create(
            volunteerId,
            name.Value,
            email.Value,
            experienceInYears.Value,
            phoneNumber.Value);

        if (name.IsFailure)
            return name.Error;

        if (email.IsFailure)
            return email.Error;

        if (experienceInYears.IsFailure)
            return experienceInYears.Error;

        if (phoneNumber.IsFailure)
            return phoneNumber.Error;

        var createResult = await _repository.CreateAsync(volunteerResult.Value, cancellationToken);

        return (Guid)createResult.Id;
    }
}