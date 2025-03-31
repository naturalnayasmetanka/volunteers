using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Volunteers.Commands.Delete.Commands;

public record HardDeleteVolunteerCommand(Guid Id) : ICommand;
