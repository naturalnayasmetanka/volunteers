using Microsoft.AspNetCore.Mvc;

namespace Shared.Framework;

[ApiController]
[Route("api")]
public abstract class ApplicationController : ControllerBase
{
    protected const string BUCKET_NAME = "photos";

    public override OkObjectResult Ok(object? value)
    {
        return base.Ok(value);
    }
}