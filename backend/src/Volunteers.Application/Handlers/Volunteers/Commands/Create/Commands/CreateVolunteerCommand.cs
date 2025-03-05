using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers.Commands.Create.DTO;

namespace Volunteers.Application.Handlers.Volunteers.Commands.Create.Commands;

public record CreateVolunteerCommand(CreateVolunteerDto VolunteerDto) : ICommand;