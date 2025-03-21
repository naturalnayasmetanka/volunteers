using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volunteers.API.Contracts.Volunteers.AddPet;
using Volunteers.API.Contracts.Volunteers.AddPetPhoto;
using Volunteers.API.Contracts.Volunteers.Create;
using Volunteers.API.Contracts.Volunteers.DeletePetPhoto;
using Volunteers.API.Contracts.Volunteers.GetVolunteers;
using Volunteers.API.Contracts.Volunteers.MovePet;
using Volunteers.API.Contracts.Volunteers.UpdateMainInfo;
using Volunteers.API.Contracts.Volunteers.UpdatePet;
using Volunteers.API.Contracts.Volunteers.UpdatePetStatus;
using Volunteers.API.Contracts.Volunteers.UpdateRequisites;
using Volunteers.API.Contracts.Volunteers.UpdateSocialNetworks;
using Volunteers.API.Extentions;
using Volunteers.API.Processors;
using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Species.Queries.CheckExists.Queries;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPet.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.AddPetPhoto.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.Create.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.Delete;
using Volunteers.Application.Handlers.Volunteers.Commands.Delete.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.DeletePetPhoto.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.MovePet.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.Restore.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateMainInfo.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePet.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePetStatus.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateSotialNetworks.Commands;
using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteer.Queries;
using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteers.Queries;
using Volunteers.Application.Models;

namespace Volunteers.API.Controllers;

[Route("api/volunteer")]
[ApiController]
public class VolunteersController : ControllerBase
{
    private const string BUCKET_NAME = "photos";

    #region Volunteer
    [HttpGet]
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> GetVolunteer(
        [FromServices] IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery> handler,
        [FromQuery] GetFilteredWithPaginationVolunteersRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> GetVolunteerById(
        [FromServices] IQueryHandler<VolunteerDTO?, GetVolunteerQuery> handler,
        [FromRoute] GetVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        var result = await handler.Handle(query, cancellationToken);
        return Ok(result.Value);
    }

    [HttpPost]
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> CreateVolunteer(
        [FromServices] ICommandHandler<Guid, CreateVolunteerCommand> handler,
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

    [HttpPatch("{volunteerId:guid}/main-info")]
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> UpdateVolunteerMainInfo(
        [FromServices] ICommandHandler<Guid, UpdateMainInfoCommand> handler,
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
    public async Task<IActionResult> UpdateVolunteerSocialNetwork(
        [FromServices] ICommandHandler<Guid, UpdateSocialNetworksCommand> handler,
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
    public async Task<IActionResult> UpdateVolunteerRequisite(
        [FromServices] ICommandHandler<Guid, UpdateRequisiteCommand> handler,
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
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> RestoreVolunteer(
        [FromServices] ICommandHandler<Guid, RestoreCommand> handler,
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
    public async Task<IActionResult> SoftDeleteVolunteer(
        [FromServices] ICommandHandler<Guid, DeleteCommand> handler,
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
    public async Task<IActionResult> HardDeleteVOlunteer(
        [FromServices] HardDeleteVolunteerHandler handler, // добавить отличительный признак софт и хард удаления
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

    #endregion

    #region Pet

    [HttpPost("{volunteerId:guid}/pet")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> CreatePetPhoto(
        [FromRoute] Guid volunteerId,
        [FromForm] AddPetRequest request,
        [FromServices] ICommandHandler<Guid, AddPetCommand> petHandler,
        [FromServices] IQueryHandler<bool, CheckExistsQuery> checkExistsSpeciesBreedHandler,
        CancellationToken cancellationToken = default)
    {
        var checkExistSpeciesBreedQuery = new CheckExistsQuery(SpeciesId: request.SpeciesId, BreedId: request.BreedId);
        var speciesBreedExist = await checkExistsSpeciesBreedHandler.Handle(checkExistSpeciesBreedQuery, cancellationToken);

        if (speciesBreedExist.IsFailure)
            return speciesBreedExist.Error
                 .ToErrorResponse();

        var addPetCommand = AddPetRequest.ToCommand(volunteerId, request);
        var addPetResult = await petHandler.Handle(addPetCommand, cancellationToken);

        if (addPetResult.IsFailure)
            return addPetResult.Error
                .ToErrorResponse();

        return Ok(addPetResult.Value);
    }

    [HttpPost("{volunteerId:guid}/pet/{petId:guid}/photo")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> CreatePetPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] AddPetPhotoRequest request,
        [FromServices] ICommandHandler<Guid, AddPetPhotoCommand> handler,
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

    [HttpPut("{volunteerId:guid}/pet/{petId:guid}")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> UpdatePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetRequest request,
        [FromServices] ICommandHandler<Guid, UpdatePetCommand> handler,
        [FromServices] IQueryHandler<bool, CheckExistsQuery> checkExistsSpeciesBreedHandler,
        CancellationToken cancellationToken = default)
    {
        var checkExistSpeciesBreedQuery = new CheckExistsQuery(SpeciesId: request.SpeciesId, BreedId: request.BreedId);
        var speciesBreedExist = await checkExistsSpeciesBreedHandler.Handle(checkExistSpeciesBreedQuery, cancellationToken);

        if (speciesBreedExist.IsFailure)
            return speciesBreedExist.Error
                 .ToErrorResponse();

        var command = UpdatePetRequest.ToCommand(volunteerId: volunteerId, petId: petId, request);
        var updatePetHandle = await handler.Handle(command, cancellationToken);

        if (updatePetHandle.IsFailure)
            return updatePetHandle.Error
                .ToErrorResponse();

        return Ok(updatePetHandle.Value);
    }

    [HttpPut("{volunteerId:guid}/pet/{petId:guid}")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> ChangeStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetStatusRequest request,
        [FromServices] ICommandHandler<Guid, UpdatePetStatusCommand> handler,
        CancellationToken cancellationToken = default)
    {
        var command = UpdatePetStatusRequest.ToCommand(volunteerId: volunteerId, petId: petId, request);
        var updatePetStatusHandle = await handler.Handle(command, cancellationToken);

        if (updatePetStatusHandle.IsFailure)
            return updatePetStatusHandle.Error
                .ToErrorResponse();

        return Ok(updatePetStatusHandle.Value);
    }

    [HttpPatch("{volunteerId:guid}/pet-position")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> UpdatePetPosition(
        [FromServices] ICommandHandler<Guid, MovePetCommand> handler,
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

    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/photo")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> DeletePetPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] ICommandHandler<string, DeletePetPhotoCommand> handler,
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

    #endregion
}