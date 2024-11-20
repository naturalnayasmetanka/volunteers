using CSharpFunctionalExtensions;
using Volunteers.Application.Volunteer.CreateVolunteer.DTO;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;
using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;

namespace Volunteers.Application.Volunteer.CreateVolunteer;

public class CreateVolunteerHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;

    public CreateVolunteerHandler(IVolunteerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId  = VolunteerId.NewVolunteerId();
        var name = Name.Create(request.VolunteerDto.Name);
        var email = Email.Create(request.VolunteerDto.Email);
        var experienceInYears = ExperienceInYears.Create(request.VolunteerDto.ExperienceInYears);
        var phoneNumber = PhoneNumber.Create(request.VolunteerDto.PhoneNumber);

        var socialNetworks = request.VolunteerDto.SocialNetworks;
        var requisites = request.VolunteerDto.VolunteerRequisites;

        if (name.IsFailure)
            _errors.Add(name.Error);

        if (email.IsFailure)
            _errors.Add(email.Error);

        if (experienceInYears.IsFailure)
            _errors.Add(experienceInYears.Error);

        if (phoneNumber.IsFailure)
            _errors.Add(phoneNumber.Error);

        if (_errors.Any())
            return _errors;

        var volunteerResult = VolunteerModel.Create(
            volunteerId,
            name.Value,
            email.Value,
            experienceInYears.Value,
            phoneNumber.Value);

        if (socialNetworks is not null)
            socialNetworks
                .ForEach(x =>
                    volunteerResult.Value.AddSocialNetwork(
                        SocialNetwork.Create(
                            x.Title,
                            x.Link
                        ).Value));

        if (requisites is not null)
            requisites
                .ForEach(x =>
                    volunteerResult.Value.AddVolunteerRequisite(
                        VolunteerRequisite.Create(
                            x.Title,
                            x.Description
                        ).Value));

        var createResult = await _repository.CreateAsync(volunteerResult.Value, cancellationToken);

        return (Guid)createResult.Id;
    }
}