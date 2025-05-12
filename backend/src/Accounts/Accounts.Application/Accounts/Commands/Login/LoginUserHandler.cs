using Accounts.Application.Accounts.Commands.Login.Commands;
using Accounts.Infrastructure.Abstractions;
using Accounts.Infrastructure.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;

namespace Accounts.Application.Accounts.Commands.Login;

public class LoginUserHandler : ICommandHandler<string, LoginUserCommand>
{
    private readonly ILogger<LoginUserHandler> _logger;
    private readonly UserManager<User> _userManager;
    private ITokenProvider _tokenProvider;

    public LoginUserHandler(
        UserManager<User> userManager,
        ILogger<LoginUserHandler> logger,
        ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _logger = logger;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<string, Error>> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var existUser = await _userManager.FindByEmailAsync(command.Email);

        if (existUser is null)
        {
            _logger.LogError($"User {command.Email} not found.");

            return Error.NotFound($"User {command.Email} not found.", "user.not.found");
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(existUser, command.Password);

        if (!isPasswordCorrect)
        {
            _logger.LogError($"Incorrect user`s {command.Email} password.");

            return Error.Failure($"Incorrect user`s {command.Email} password.", "incorrect.password");
        }

        var token = _tokenProvider.GenerateAccessToken(existUser);

        _logger.LogInformation($"User {command.Email} successfully logged in.");

        return token;
    }
}
