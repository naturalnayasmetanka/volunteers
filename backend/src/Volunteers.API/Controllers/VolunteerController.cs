using Microsoft.AspNetCore.Mvc;
using Volunteers.Application.Volunteer.CreateVolunteer;

namespace Volunteers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public IEnumerable<string> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        return new string[] { "value1", "value2" };
    }
}