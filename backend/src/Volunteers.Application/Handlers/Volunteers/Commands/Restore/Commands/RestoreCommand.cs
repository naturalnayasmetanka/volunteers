using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Volunteers.Commands.Restore.Commands;

public record RestoreCommand(Guid Id) : ICommand;