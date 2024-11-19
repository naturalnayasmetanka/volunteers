using Microsoft.AspNetCore.Mvc;
using Volunteers.API.Extentions;
using Volunteers.Application.Volunteer.CreateVolunteer;

namespace Volunteers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest createRequest,
        CancellationToken cancellationToken = default)
    {
        var createResult = await handler.Handle(createRequest, cancellationToken); 

        if (createResult.IsFailure)
            return createResult.Error.ToResponse();

        return Created();
    }
} 