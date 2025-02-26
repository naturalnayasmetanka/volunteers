using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Volunteers.Commands.Restore.Commands;

public record RestoreCommand(Guid Id) : ICommand;