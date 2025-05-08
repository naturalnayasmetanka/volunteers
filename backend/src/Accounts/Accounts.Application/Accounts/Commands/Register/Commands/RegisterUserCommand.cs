using Shared.Core.Abstractions.Handlers;

namespace Accounts.Application.Accounts.Commands.Register.Commands;

public record RegisterUserCommand(string Email, string UserName, string Password): ICommand;
