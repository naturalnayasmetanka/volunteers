using Microsoft.AspNetCore.Mvc;
using Shared.Framework;
using Swashbuckle.AspNetCore.Annotations;

namespace Accounts.Presentation.Controllers;

public class AccountsController : ApplicationController
{
    [HttpPost("login")]
    [SwaggerOperation(Tags = ["Accounts"])]
    public async Task<IActionResult> Login(
        CancellationToken cancellationToken = default)
    {
        return Ok();
    }

    [HttpPost("register")]
    [SwaggerOperation(Tags = ["Accounts"])]
    public async Task<IActionResult> Register(
        CancellationToken cancellationToken = default)
    {
        return Ok();
    }
}
