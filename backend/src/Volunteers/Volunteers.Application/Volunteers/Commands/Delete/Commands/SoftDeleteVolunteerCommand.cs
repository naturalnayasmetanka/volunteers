using Shared.Core.Abstractions.Handlers;

namespace Volunteers.Application.Volunteers.Commands.Delete.Commands;

public record SoftDeleteVolunteerCommand(Guid Id) : ICommand;
