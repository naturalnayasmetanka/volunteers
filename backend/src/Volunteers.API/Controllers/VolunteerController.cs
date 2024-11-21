using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Volunteers.API.Extentions;
using Volunteers.Application.Volunteer.CreateVolunteer;
using Volunteers.Application.Volunteers.CreateVolunteer.RequestModels;

namespace Volunteers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromServices] IValidator<CreateVolunteerRequest> validator,
        [FromBody] CreateVolunteerRequest createRequest,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(createRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors
                .FromFluientToErrorResponse();
        }

        var createResult = await handler.Handle(createRequest, cancellationToken);

        if (createResult.IsFailure)
            return createResult.Error
                .ToErrorResponse();

        return Created();
    }
}