using Microsoft.AspNetCore.Mvc;
using Volunteers.API.Extentions;
using Volunteers.Application.Volunteer.CreateVolunteer;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerDto volunteerDto,
        CancellationToken cancellationToken = default)
    {
        var createRequest = new CreateVolunteerRequest(volunteerDto);
        var createResult = await handler.Handle(createRequest, cancellationToken);

        if (createResult.Error.Type == ErrorType.Validation)
            return BadRequest(createResult.Error);

        if (createResult.IsFailure)
            return createResult.Error.ToResponse();

        return Created();
    }
}