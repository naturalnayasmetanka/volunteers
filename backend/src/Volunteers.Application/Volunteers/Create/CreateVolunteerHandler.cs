using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Volunteers.CreateVolunteer.Commands;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;
using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;

namespace Volunteers.Application.Volunteer.CreateVolunteer;

public class CreateVolunteerHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(
        IVolunteerRepository repository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var name = Name.Create(command.VolunteerDto.Name).Value;
        var email = Email.Create(command.VolunteerDto.Email).Value;
        var experienceInYears = ExperienceInYears.Create(command.VolunteerDto.ExperienceInYears).Value;
        var phoneNumber = PhoneNumber.Create(command.VolunteerDto.PhoneNumber).Value;

        var socialNetworks = command.VolunteerDto.SocialNetworks;
        var requisites = command.VolunteerDto.VolunteerRequisites;

        var volunteerResult = VolunteerModel.Create(
            volunteerId,
            name,
            email,
            experienceInYears,
            phoneNumber).Value;

        if (socialNetworks is not null)
            socialNetworks
                .ForEach(x =>
                    volunteerResult.AddSocialNetwork(
                        SocialNetwork.Create(
                            x.Title,
                            x.Link
                        ).Value));

        if (requisites is not null)
            requisites
                .ForEach(x =>
                    volunteerResult.AddVolunteerRequisite(
                        VolunteerRequisite.Create(
                            x.Title,
                            x.Description
                        ).Value));

        var createResult = await _repository
            .CreateAsync(volunteerResult, cancellationToken);

        _logger.LogInformation("Volunteer was created with id: {0}", volunteerId.Value);

        return (Guid)createResult.Id;
    }
}