using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Core.Models;
using Shared.Framework;
using Shared.Framework.Extentions;
using Shared.Framework.Processors;
using Species.Contracts.Species;
using Species.Contracts.Species.Requests.Species.CheckExists;
using Swashbuckle.AspNetCore.Annotations;
using Volunteers.Application.Pets.Queries.GetPets.Queries;
using Volunteers.Application.Volunteers.Commands.AddPet.Commands;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.Create.Commands;
using Volunteers.Application.Volunteers.Commands.Delete.Commands;
using Volunteers.Application.Volunteers.Commands.DeletePet.Commands;
using Volunteers.Application.Volunteers.Commands.DeletePetPhoto.Commands;
using Volunteers.Application.Volunteers.Commands.MovePet.Commands;
using Volunteers.Application.Volunteers.Commands.Restore.Commands;
using Volunteers.Application.Volunteers.Commands.SetMainPetPhoto;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.Commands;
using Volunteers.Application.Volunteers.Commands.UpdatePet.Commands;
using Volunteers.Application.Volunteers.Commands.UpdatePetStatus.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks.Commands;
using Volunteers.Application.Volunteers.Queries.GetVolunteer.Queries;
using Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;
using Volunteers.Contracts.Pets.Requests.Pets.GetPets;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.AddPet;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.AddPetPhoto;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.Create;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.DeletePetPhoto;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.GetVolunteers;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.MovePet;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.SetMainPetPhoto;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.UpdateMainInfo;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.UpdatePet;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.UpdatePetStatus;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.UpdateRequisites;
using Volunteers.Contracts.Volunteers.Requests.Volunteers.UpdateSocialNetworks;

namespace Volunteers.Presentation.Controllers;

public class VolunteersController : ApplicationController
{
    #region Volunteer
    [HttpGet("volunteers")]
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> GetVolunteers(
        [FromServices] IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery> handler,
        [FromQuery] GetFilteredWithPaginationVolunteersRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("volunteer/{id:guid}")]
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

    [HttpPost("volunteer")]
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

    [HttpPatch("volunteer/{volunteerId:guid}/main-info")]
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

    [HttpPatch("volunteer/{volunteerId:guid}/social-network")]
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

    [HttpPatch("volunteer/{volunteerId:guid}/requisites")]
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

    [HttpPatch("volunteer/{volunteerId:guid}/restore")]
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

    [HttpDelete("volunteer/{volunteerId:guid}/volunteer-soft")]
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> SoftDeleteVolunteer(
        [FromServices] ICommandHandler<Guid, SoftDeleteVolunteerCommand> handler,
        [FromRoute] Guid volunteerId,
        CancellationToken cancellationToken = default)
    {
        var deleteCommand = new SoftDeleteVolunteerCommand(volunteerId);
        var softDeleteResult = await handler.Handle(deleteCommand, cancellationToken);

        if (softDeleteResult.IsFailure)
            return softDeleteResult.Error
                .ToErrorResponse();

        return Ok(volunteerId);
    }

    [HttpDelete("volunteer/{volunteerId:guid}/volunteer-hard")]
    [SwaggerOperation(Tags = ["Volunteer"])]
    public async Task<IActionResult> HardDeleteVolunteer(
        [FromServices] ICommandHandler<Guid, HardDeleteVolunteerCommand> handler,
        [FromRoute] Guid volunteerId,
        CancellationToken cancellationToken = default)
    {
        var deleteCommand = new HardDeleteVolunteerCommand(volunteerId);
        var hardDeleteResult = await handler.Handle(deleteCommand, cancellationToken);

        if (hardDeleteResult.IsFailure)
            return hardDeleteResult.Error
                .ToErrorResponse();

        return Ok(volunteerId);
    }

    #endregion

    #region Pet
    [HttpGet("volunteer/{volunteerId:guid}/pets")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> GetPets(
        [FromRoute] Guid volunteerId,
        [FromQuery] GetFilteredWithPaginationPetsRequest request,
        [FromServices] IQueryHandler<PagedList<PetDTO>, GetFilteredWithPaginationPetsQuery> handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery(volunteerId);
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost("volunteer/{volunteerId:guid}/pet")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> CreatePet(
        [FromRoute] Guid volunteerId,
        [FromForm] AddPetRequest request,
        [FromServices] ICommandHandler<Guid, AddPetCommand> petHandler,
        [FromServices] ISpeciesContract checkExistsSpeciesBreedContract,
        CancellationToken cancellationToken = default)
    {
        var checkExistSpeciesBreedRequest = new CheckExistsRequest(SpeciesId: request.SpeciesId, BreedId: request.BreedId);
        var speciesBreedExist = await checkExistsSpeciesBreedContract.CheckExists(checkExistSpeciesBreedRequest, cancellationToken);

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

    [HttpPost("volunteer/{volunteerId:guid}/pet/{petId:guid}/photo")]
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

    [HttpPost("volunteer/{volunteerId:guid}/pet/{petId:guid}", Name = nameof(UpdatePet))]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> UpdatePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetRequest request,
        [FromServices] ICommandHandler<Guid, UpdatePetCommand> handler,
        [FromServices] ISpeciesContract checkExistsSpeciesBreedContract,
        CancellationToken cancellationToken = default)
    {
        var checkExistSpeciesBreedRequest = new CheckExistsRequest(SpeciesId: request.SpeciesId, BreedId: request.BreedId);
        var speciesBreedExist = await checkExistsSpeciesBreedContract.CheckExists(checkExistSpeciesBreedRequest, cancellationToken);

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

    [HttpPost("volunteer/{volunteerId:guid}/pet/{petId:guid}/set-main-photo", Name = nameof(SetMainPhoto))]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> SetMainPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] SetMainPetPhotoRequest request,
        [FromServices] SetMainPetPhotoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = SetMainPetPhotoRequest.ToCommand(volunteerId: volunteerId, petId: petId, request: request);
        var setMainPetPhotoResult = await handler.Handle(command: command, cancellationToken: cancellationToken);

        if (setMainPetPhotoResult.IsFailure)
            return setMainPetPhotoResult.Error
                   .ToErrorResponse();

        return Ok(setMainPetPhotoResult.Value);
    }

    [HttpPatch("volunteer/{volunteerId:guid}/pet/{petId:guid}", Name = nameof(ChangeStatus))]
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

    [HttpPatch("volunteer/{volunteerId:guid}/pet-position")]
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

    [HttpDelete("volunteer/{volunteerId:guid}/pet/{petId:guid}/photo")]
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

    [HttpDelete("volunteer/{volunteerId:guid}/pet-soft")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> SoftDeletePet(
        [FromServices] ICommandHandler<Guid, SoftDeletePetCommand> handler,
        [FromRoute] Guid volunteerId,
        [FromBody] Guid petId,
        CancellationToken cancellationToken = default)
    {
        var deleteCommand = new SoftDeletePetCommand(VolunteerId: volunteerId, PetId: petId);
        var softDeleteResult = await handler.Handle(deleteCommand, cancellationToken);

        if (softDeleteResult.IsFailure)
            return softDeleteResult.Error
                .ToErrorResponse();

        return Ok(volunteerId);
    }

    [HttpDelete("volunteer/{volunteerId:guid}/pet-hard")]
    [SwaggerOperation(Tags = ["Pet"])]
    public async Task<IActionResult> HardDeletePet(
        [FromServices] ICommandHandler<Guid, HardDeletePetCommand> handler,
        [FromRoute] Guid volunteerId,
        [FromBody] Guid petId,
        CancellationToken cancellationToken = default)
    {
        var deleteCommand = new HardDeletePetCommand(VolunteerId: volunteerId, PetId: petId, BucketName: BUCKET_NAME);
        var hardDeleteResult = await handler.Handle(deleteCommand, cancellationToken);

        if (hardDeleteResult.IsFailure)
            return hardDeleteResult.Error
                .ToErrorResponse();

        return Ok(volunteerId);
    }

    #endregion
}
