using Accounts.Application.Accounts.Commands.Login.Commands;
using Microsoft.AspNetCore.Identity.Data;

namespace Accounts.Contracts.Requests;

public record LoginUserRequest(string Email, string Password)
{
    public static LoginUserCommand ToCommand(LoginRequest request)
    {
        var command = new LoginUserCommand(Email: request.Email, Password: request.Password);

        return command;
    }
}
