namespace Volunteers.Application.Volunteers.UpdateMainInfo.DTO;

public record UpdateMainInfoDto(
    string Name, 
    string Email, 
    double ExperienceInYears,
    int PhoneNumber);