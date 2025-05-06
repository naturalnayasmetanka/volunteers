using Shared.Core.Abstractions.Handlers;
using Volunteers.Application.Volunteers.Commands.Create.DTO;

namespace Volunteers.Application.Volunteers.Commands.Create.Commands;

public record CreateVolunteerCommand(CreateVolunteerDto VolunteerDto) : ICommand;
