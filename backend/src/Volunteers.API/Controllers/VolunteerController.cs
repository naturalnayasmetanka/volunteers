using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volunteers.API.Contracts.Volunteers.AddPet;
using Volunteers.API.Contracts.Volunteers.AddPetPhoto;
using Volunteers.API.Contracts.Volunteers.Create;
using Volunteers.API.Contracts.Volunteers.DeletePetPhoto;
using Volunteers.API.Contracts.Volunteers.MovePet;
using Volunteers.API.Contracts.Volunteers.UpdateMainInfo;
using Volunteers.API.Contracts.Volunteers.UpdateRequisites;
using Volunteers.API.Contracts.Volunteers.UpdateSocialNetworks;
using Volunteers.API.Extentions;
using Volunteers.API.Processors;
using Volunteers.Application.Volunteer.CreateVolunteer;
using Volunteers.Application.Volunteers.AddPet;
using Volunteers.Application.Volunteers.AddPetPhoto;
using Volunteers.Application.Volunteers.Delete;
using Volunteers.Application.Volunteers.Delete.Commands;
using Volunteers.Application.Volunteers.DeletePetPhoto;
using Volunteers.Application.Volunteers.MovePet;
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
    private const string BUCKET_NAME = "photos";

    [HttpGet]
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken = default)
    {

        return Ok();
    }

    [HttpPost]
    [SwaggerOperation(Tags = ["Volunteer"])]
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
    [SwaggerOperation(Tags = ["Pet"])]
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

    [HttpPost("{volunteerId:guid}/pet/{petId:guid}/photo")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> Create(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] AddPetPhotoRequest request,
        [FromServices] AddPetPhotoHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var fileProcessor = new FormFileProcessor();
        var petPhoto = fileProcessor.Process(BUCKET_NAME, request.Photo);

        var addPetPhotoCommand = AddPetPhotoRequest.ToCommand(
            volunteerId: volunteerId,
            petId: petId,
            request: request,
            petPhoto: petPhoto);

        var addPhotoResult = await handler.Handle(addPetPhotoCommand, cancellationToken);

        if (addPhotoResult.IsFailure)
            return addPhotoResult.Error
                .ToErrorResponse();

        return Ok(addPhotoResult.Value);
    }

    [HttpPatch("{volunteerId:guid}/main-info")]
    [SwaggerOperation(Tags = ["Volunteer"])]
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
    [SwaggerOperation(Tags = ["Volunteer"])]
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

        return Ok(socialUpdateResult.Value);
    }

    [HttpPatch("{volunteerId:guid}/requisites")]
    [SwaggerOperation(Tags = ["Volunteer"])]
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

    [HttpPatch("{volunteerId:guid}/pet-position")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> Update(
        [FromServices] MovePetHandler handler,
        [FromBody] MovePetRequest request,
        [FromRoute] Guid volunteerId,
        CancellationToken cancellationToken = default)

    {
        var command = MovePetRequest.ToCommand(volunteerId, request);
        var movePetHandle = await handler.Handle(command, cancellationToken);

        if (movePetHandle.IsFailure)
            return movePetHandle.Error
                .ToErrorResponse();

        return Ok(movePetHandle.Value);
    }

    [HttpPatch("{volunteerId:guid}/restore")]
    [SwaggerOperation(Tags = ["Volunteer"])]
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
    [SwaggerOperation(Tags = ["Volunteer"])]
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
    [SwaggerOperation(Tags = ["Volunteer"])]
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

    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/photo")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetPhotoHandler handler,
        [FromBody] DeletePetPhotoRequest request,
        CancellationToken cancellationToken = default)
    {
        var deletePetPhotoCommand = DeletePetPhotoRequest.ToCommand(
            volunteerId: volunteerId,
            petId: petId,
            BUCKET_NAME: BUCKET_NAME,
            request: request);

        var deleteResult = await handler.Handle(deletePetPhotoCommand, cancellationToken);

        if (deleteResult.IsFailure)
            return deleteResult.Error
                .ToErrorResponse();

        return Ok(deleteResult.Value);
    }
}