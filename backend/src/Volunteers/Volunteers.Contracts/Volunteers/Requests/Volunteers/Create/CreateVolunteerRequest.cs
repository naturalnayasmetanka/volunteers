using Volunteers.Application.Handlers.Volunteers.Commands.Create.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.Create.DTO;

namespace Volunteers.API.Contracts.Volunteers.Create;

public record CreateVolunteerRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public double ExperienceInYears { get; set; }
    public int PhoneNumber { get; set; }
    List<SocialNetworkDto>? SocialNetworks { get; set; }
    List<VolunteerRequisiteDto>? VolunteerRequisites { get; set; }

    public static CreateVolunteerCommand ToCommand(CreateVolunteerRequest request)
    {
        var command = new CreateVolunteerCommand(new CreateVolunteerDto(
            Name: request.Name,
            Email: request.Email,
            ExperienceInYears: request.ExperienceInYears,
            PhoneNumber: request.PhoneNumber,
            SocialNetworks: request.SocialNetworks,
            VolunteerRequisites: request.VolunteerRequisites
        ));

        return command;
    }
};
