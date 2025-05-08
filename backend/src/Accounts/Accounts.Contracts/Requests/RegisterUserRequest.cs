using Accounts.Application.Accounts.Commands.Register.Commands;

namespace Accounts.Contracts.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public static RegisterUserCommand ToCommand(RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(
            Email: request.Email,
            UserName: request.UserName,
            Password: request.Password);

        return command;
    }
};
