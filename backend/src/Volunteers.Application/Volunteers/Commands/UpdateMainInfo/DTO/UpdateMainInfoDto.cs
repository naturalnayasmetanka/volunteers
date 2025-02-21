namespace Volunteers.Application.Volunteers.Commands.UpdateMainInfo.DTO;

public record UpdateMainInfoDto(
    string Name,
    string Email,
    double ExperienceInYears,
    int PhoneNumber);