using Volunteers.Application.Abstractions;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.DTO;

namespace Volunteers.Application.Volunteers.Commands.UpdateMainInfo.Commands;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    UpdateMainInfoDto MainInfoDto) : ICommand;