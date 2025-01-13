using Microsoft.AspNetCore.Mvc;
using Volunteers.API.Contracts.Volunteers.AddPet;
using Volunteers.API.Contracts.Volunteers.AddPetPhoto;
using Volunteers.API.Contracts.Volunteers.Create;
using Volunteers.API.Contracts.Volunteers.UpdateMainInfo;
using Volunteers.API.Contracts.Volunteers.UpdateRequisites;
using Volunteers.API.Contracts.Volunteers.UpdateSocialNetworks;
using Volunteers.API.Extentions;
using Volunteers.Application.Volunteer.CreateVolunteer;
using Volunteers.Application.Volunteers.AddPet;
using Volunteers.Application.Volunteers.AddPetPhoto;
using Volunteers.Application.Volunteers.Delete;
using Volunteers.Application.Volunteers.Delete.Commands;
using Volunteers.Application.Volunteers.Restore;
using Volunteers.Application.Volunteers.Restore.Commands;
using Volunteers.Application.Volunteers.UpdateMainInfo;
using Volunteers.Application.Volunteers.UpdateRequisites;
using Volunteers.Application.Volunteers.UpdateSotialNetworks;

namespace Volunteers.API.Controllers;

[Route("api/volunteer")]
[ApiController]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var createVolunteerCommand = CreateVolunteerRequest.ToCommand(request);
        var createResult = await handler.Handle(createVolunteerCommand, cancellationToken);

        if (createResult.IsFailure)
            return createResult.Error
                .ToErrorResponse();

        return Created();
    }

    [HttpPost("{volunteerId:guid}/pet")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid volunteerId,
        [FromForm] AddPetRequest request,
        [FromServices] AddPetVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var addPetCommand = AddPetRequest.ToCommand(volunteerId, request);
        var addPetResult = await handler.Handle(addPetCommand, cancellationToken);

        if (addPetResult.IsFailure)
            return addPetResult.Error
                .ToErrorResponse();

        return Ok(addPetResult.Value);
    }

    [HttpPost("{volunteerId:guid}/pet-photo")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid volunteerId,
        [FromForm] AddPetPhotoRequest request,
        [FromServices] AddPetPhotoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var addPetPhotoCommand = await AddPetPhotoRequest.ToCommandAsync(volunteerId, request);
        var addPhotoResult = await handler.Handle(addPetPhotoCommand, cancellationToken);

        if (addPhotoResult.IsFailure)
            return addPhotoResult.Error
                .ToErrorResponse();

        return Ok(addPhotoResult.Value);
    }



    [HttpPatch("{volunteerId:guid}/main-info")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        [FromRoute] Guid volunteerId,
        CancellationToken cancellationToken = default)
    {
        var updateMainInfoCommand = UpdateMainInfoRequest.ToCommand(volunteerId, request);
        var mainInfoUpdateResult = await handler.Handle(updateMainInfoCommand, cancellationToken);

        if (mainInfoUpdateResult.IsFailure)
            return mainInfoUpdateResult.Error
                .ToErrorResponse();

        return Ok(mainInfoUpdateResult.Value);
    }

    [HttpPatch("{volunteerId:guid}/social-network")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateSotialNetworksHandler handler,
        [FromBody] UpdateSocialListRequest request,
        [FromRoute] Guid volunteerId,
        CancellationToken cancellationToken = default)
    {
        var updateSocialInfoCommand = UpdateSocialListRequest.ToCommand(volunteerId, request);
        var socialUpdateResult = await handler.Handle(updateSocialInfoCommand, cancellationToken);

        if (socialUpdateResult.IsFailure)
            return socialUpdateResult.Error
                .ToErrorResponse();

        return Ok(socialUpdateResult);
    }

    [HttpPatch("{volunteerId:guid}/requisites")]
    public async Task<IActionResult> Update(
    [FromServices] UpdateRequisitesHandler handler,
    [FromBody] UpdateRequisiteRequest request,
    [FromRoute] Guid volunteerId,
    CancellationToken cancellationToken = default)
    {
        var updateRequisitesCommand = UpdateRequisiteRequest.ToCommand(volunteerId, request);
        var requisitesUpdateResult = await handler.Handle(updateRequisitesCommand, cancellationToken);

        if (requisitesUpdateResult.IsFailure)
            return requisitesUpdateResult.Error
                .ToErrorResponse();

        return Ok(requisitesUpdateResult);
    }

    [HttpPatch("{volunteerId:guid}/restore")]
    public async Task<IActionResult> Restore(
    [FromServices] RestoreVolunteerHandler handler,
    [FromRoute] Guid volunteerId,
    CancellationToken cancellationToken = default)
    {
        var restoreCommand = new RestoreCommand(volunteerId);
        var restoreResult = await handler.Handle(restoreCommand, cancellationToken);

        if (restoreResult.IsFailure)
            return restoreResult.Error
                .ToErrorResponse();

        return Ok(volunteerId);
    }

    [HttpDelete("{volunteerId:guid}")]
    public async Task<IActionResult> Delete(
    [FromServices] SoftDeleteVolunteerHandler handler,
    [FromRoute] Guid volunteerId,
    CancellationToken cancellationToken = default)
    {
        var deleteCommand = new DeleteCommand(volunteerId);
        var softDeleteResult = await handler.Handle(deleteCommand, cancellationToken);

        if (softDeleteResult.IsFailure)
            return softDeleteResult.Error
                .ToErrorResponse();

        return Ok(volunteerId);
    }

    [HttpDelete("{volunteerId:guid}/hard")]
    public async Task<IActionResult> Delete(
    [FromServices] HardDeleteVolunteerHandler handler,
    [FromRoute] Guid volunteerId,
    CancellationToken cancellationToken = default)
    {
        var deleteCommand = new DeleteCommand(volunteerId);
        var hardDeleteResult = await handler.Handle(deleteCommand, cancellationToken);

        if (hardDeleteResult.IsFailure)
            return hardDeleteResult.Error
                .ToErrorResponse();

        return Ok(volunteerId);
    }
}