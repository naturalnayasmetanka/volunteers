namespace Volunteers.Application.Volunteer.CreateVolunteer.DTO;

public record CreateVolunteerDto(
    string Name,
    string Email,
    double ExperienceInYears,
    int PhoneNumber,
    List<SocialNetworkDto>? SocialNetworks,
    List<VolunteerRequisiteDto>? VolunteerRequisites);