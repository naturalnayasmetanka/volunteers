namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdateMainInfo.DTO;

public record UpdateMainInfoDto(
    string Name,
    string Email,
    double ExperienceInYears,
    int PhoneNumber);