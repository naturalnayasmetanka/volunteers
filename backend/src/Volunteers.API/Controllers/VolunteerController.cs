using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Volunteers.API.Extentions;
using Volunteers.Application.Volunteer.CreateVolunteer;
using Volunteers.Application.Volunteer.CreateVolunteer.DTO;
using Volunteers.Application.Volunteers.CreateVolunteer.RequestModels;
using Volunteers.Application.Volunteers.UpdateMainInfo;
using Volunteers.Application.Volunteers.UpdateMainInfo.DTO;
using Volunteers.Application.Volunteers.UpdateMainInfo.RequestModels;

namespace Volunteers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromServices] IValidator<CreateVolunteerDto> validator,
        [FromBody] CreateVolunteerDto createDto,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(createDto, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.Errors
                    .FromFluientToErrorResponse();

        var createRequest = new CreateVolunteerRequest(createDto);
        var createResult = await handler.Handle(createRequest, cancellationToken);

        if (createResult.IsFailure)
            return createResult.Error
                .ToErrorResponse();

        return Created();
    }

    [HttpPatch("{id:guid}/main-info")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateMainInfoHandler handler,
        [FromServices] IValidator<UpdateMainInfoDto> validator,
        [FromBody] UpdateMainInfoDto mainInfoDto,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(mainInfoDto, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.Errors
                    .FromFluientToErrorResponse();

        var updateMainInfoRequest = new UpdateMainInfoRequest(id, mainInfoDto);
        var mainInfoUpdateResult = await handler.Handle(updateMainInfoRequest, cancellationToken);

        if (mainInfoUpdateResult.IsFailure)
            return mainInfoUpdateResult.Error
                .ToErrorResponse();

        return Ok();
    }
}