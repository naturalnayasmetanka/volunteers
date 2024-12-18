using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Volunteers.API.Extentions;
using Volunteers.Application.Volunteer.CreateVolunteer;
using Volunteers.Application.Volunteer.CreateVolunteer.DTO;
using Volunteers.Application.Volunteers.AddPet;
using Volunteers.Application.Volunteers.CreateVolunteer.RequestModels;
using Volunteers.Application.Volunteers.Delete;
using Volunteers.Application.Volunteers.Delete.RequestModels;
using Volunteers.Application.Volunteers.Restore;
using Volunteers.Application.Volunteers.Restore.RequestModels;
using Volunteers.Application.Volunteers.UpdateMainInfo;
using Volunteers.Application.Volunteers.UpdateMainInfo.DTO;
using Volunteers.Application.Volunteers.UpdateMainInfo.RequestModels;
using Volunteers.Application.Volunteers.UpdateRequisites;
using Volunteers.Application.Volunteers.UpdateRequisites.DTO;
using Volunteers.Application.Volunteers.UpdateRequisites.RequestModels;
using Volunteers.Application.Volunteers.UpdateSotialNetworks;
using Volunteers.Application.Volunteers.UpdateSotialNetworks.DTO;
using Volunteers.Application.Volunteers.UpdateSotialNetworks.RequestModels;

namespace Volunteers.API.Controllers;

[Route("api/volunteer")]
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

    [HttpPost("{volunteerId:guid}/pet")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid volunteerId,
        [FromServices] AddPetVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        return Ok();
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

        return Ok(mainInfoUpdateResult);
    }

    [HttpPatch("{id:guid}/social-network")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateSotialNetworksHandler handler,
        [FromServices] IValidator<UpdateSocialListDto> validator,
        [FromBody] UpdateSocialListDto socialDto,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(socialDto, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.Errors
                    .FromFluientToErrorResponse();

        var updateSocialInfoRequest = new UpdateSocialRequest(id, socialDto);
        var socialUpdateResult = await handler.Handle(updateSocialInfoRequest, cancellationToken);

        if (socialUpdateResult.IsFailure)
            return socialUpdateResult.Error
                .ToErrorResponse();

        return Ok(socialDto);
    }

    [HttpPatch("{id:guid}/requisites")]
    public async Task<IActionResult> Update(
    [FromServices] UpdateRequisitesHandler handler,
    [FromServices] IValidator<UpdateRequisiteListDTO> validator,
    [FromBody] UpdateRequisiteListDTO requisitesDto,
    [FromRoute] Guid id,
    CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(requisitesDto, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.Errors
                    .FromFluientToErrorResponse();

        var updateRequisitesRequest = new UpdateRequisiteRequest(id, requisitesDto);
        var requisitesUpdateResult = await handler.Handle(updateRequisitesRequest, cancellationToken);

        if (requisitesUpdateResult.IsFailure)
            return requisitesUpdateResult.Error
                .ToErrorResponse();

        return Ok(requisitesDto);
    }

    [HttpPatch("{id:guid}/restore")]
    public async Task<IActionResult> Restore(
    [FromServices] RestoreVolunteerHandler handler,
    [FromRoute] Guid id,
    CancellationToken cancellationToken = default)
    {
        var requestId = new RestoreRequest(id);
        var restoreResult = await handler.Handle(requestId, cancellationToken);

        if (restoreResult.IsFailure)
            return restoreResult.Error
                .ToErrorResponse();

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
    [FromServices] SoftDeleteVolunteerHandler handler,
    [FromRoute] Guid id,
    CancellationToken cancellationToken = default)
    {
        var requestId = new DeleteRequest(id);
        var softDeleteResult = await handler.Handle(requestId, cancellationToken);

        if (softDeleteResult.IsFailure)
            return softDeleteResult.Error
                .ToErrorResponse();

        return Ok(id);
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<IActionResult> Delete(
    [FromServices] HardDeleteVolunteerHandler handler,
    [FromRoute] Guid id,
    CancellationToken cancellationToken = default)
    {
        var requestId = new DeleteRequest(id);
        var hardDeleteResult = await handler.Handle(requestId, cancellationToken);

        if (hardDeleteResult.IsFailure)
            return hardDeleteResult.Error
                .ToErrorResponse();

        return Ok(id);
    }
}