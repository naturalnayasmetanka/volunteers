using Accounts.Application.Accounts.Commands.Login.Commands;

namespace Accounts.Contracts.Requests;

public record LoginUserRequest(string Email, string Password)
{
    public static LoginUserCommand ToCommand(LoginUserRequest request)
    {
        var command = new LoginUserCommand(Email: request.Email, Password: request.Password);

        return command;
    }
}
