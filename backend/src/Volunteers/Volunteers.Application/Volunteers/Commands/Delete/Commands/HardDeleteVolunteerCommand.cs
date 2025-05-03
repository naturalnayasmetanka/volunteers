using Shared.Core.Abstractions.Handlers;

namespace Volunteers.Application.Volunteers.Commands.Delete.Commands;

public record HardDeleteVolunteerCommand(Guid Id) : ICommand;
