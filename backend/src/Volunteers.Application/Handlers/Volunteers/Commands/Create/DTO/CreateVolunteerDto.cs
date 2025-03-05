namespace Volunteers.Application.Handlers.Volunteers.Commands.Create.DTO;

public record CreateVolunteerDto(
    string Name,
    string Email,
    double ExperienceInYears,
    int PhoneNumber,
    List<SocialNetworkDto>? SocialNetworks,
    List<VolunteerRequisiteDto>? VolunteerRequisites);