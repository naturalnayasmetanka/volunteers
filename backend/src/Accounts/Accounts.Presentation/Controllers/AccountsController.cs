using Accounts.Application.Accounts.Commands.Login.Commands;
using Accounts.Application.Accounts.Commands.Register.Commands;
using Accounts.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions.Handlers;
using Shared.Framework;
using Shared.Framework.Extentions;
using Swashbuckle.AspNetCore.Annotations;

namespace Accounts.Presentation.Controllers;

public class AccountsController : ApplicationController
{
    [HttpPost("login")]
    [SwaggerOperation(Tags = ["Accounts"])]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] ICommandHandler<string, LoginUserCommand> handler,
        CancellationToken cancellationToken = default)
    {
        var command = LoginUserRequest.ToCommand(request);

        var loginResult = await handler.Handle(command, cancellationToken);

        if (loginResult.IsFailure)
            return loginResult.Error
                      .ToErrorResponse();


        return Ok(loginResult.Value);
    }

    [HttpPost("registration")]
    [SwaggerOperation(Tags = ["Accounts"])]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request, 
        [FromServices] ICommandHandler<string, RegisterUserCommand> handler,
        CancellationToken cancellationToken = default)
    {
        var command = RegisterUserRequest.ToCommand(request);

        var regusterResult = await handler.Handle(command);

        if (regusterResult.IsFailure)
            return regusterResult.Error
                    .ToErrorResponse();

        return Ok(regusterResult.Value);
    }

    [Authorize]
    [HttpGet("aboba")]
    public IActionResult Test()
    {
        return Ok("aboba");
    }
}
