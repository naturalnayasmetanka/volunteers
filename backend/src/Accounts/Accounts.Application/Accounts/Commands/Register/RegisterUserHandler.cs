using Accounts.Application.Accounts.Commands.Register.Commands;
using Accounts.Infrastructure.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;

namespace Accounts.Application.Accounts.Commands.Register;

public class RegisterUserHandler : ICommandHandler<string, RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(
        UserManager<User> userManager,
         ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Result<string, Error>> Handle(
        RegisterUserCommand command, 
        CancellationToken cancellationToken = default)
    {
        var existedUser = await _userManager.FindByEmailAsync(command.Email);

        if (existedUser is not null)
        {
            _logger.LogError($"User {command.UserName} already exist.");

            return Errors.General.AlreadyExists($"User {command.UserName} already exist.");
        }

        var newUser = new User
        {
            Email = command.Email,
            UserName = command.UserName,
        };

        var userResult = await _userManager.CreateAsync(newUser, command.Password);

        if (!userResult.Succeeded)
        {
            _logger.LogError($"User {command.UserName} was not created. Error {userResult.Errors.First().Description}");

            return Errors.General.InternalServerError($"User {command.UserName} was not created. Error {userResult.Errors.First().Description}");
        }

        _logger.LogInformation($"User {command.UserName} was created");

        return command.UserName;
    }
}
