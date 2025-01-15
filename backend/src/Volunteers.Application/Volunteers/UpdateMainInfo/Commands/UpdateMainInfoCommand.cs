using Volunteers.Application.Volunteers.UpdateMainInfo.DTO;

namespace Volunteers.Application.Volunteers.UpdateMainInfo.Commands;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    UpdateMainInfoDto MainInfoDto);