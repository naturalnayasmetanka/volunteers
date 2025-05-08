
using Shared.Core.Abstractions.Handlers;

namespace Accounts.Application.Accounts.Commands.Login.Commands;

public record LoginUserCommand(string Email, string Password) : ICommand;
