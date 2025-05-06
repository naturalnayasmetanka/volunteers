using Shared.Core.Abstractions.Handlers;

namespace Volunteers.Application.Volunteers.Commands.Restore.Commands;

public record RestoreCommand(Guid Id) : ICommand;
