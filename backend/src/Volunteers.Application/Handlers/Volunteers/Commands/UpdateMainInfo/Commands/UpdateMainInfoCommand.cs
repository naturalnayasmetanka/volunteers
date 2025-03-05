using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateMainInfo.DTO;

namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdateMainInfo.Commands;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    UpdateMainInfoDto MainInfoDto) : ICommand;