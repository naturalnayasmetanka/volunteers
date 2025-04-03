using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Pets.Queries.GetPet.Queries;
using Volunteers.Application.Handlers.Pets.Queries.GetPets.Queries;
using Volunteers.Application.Handlers.Volunteers.Commands.GetPresignedLinkPhoto;
using Volunteers.Application.Models;

namespace Volunteers.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private const string BUCKET_NAME = "photos";

        [HttpGet("pets/{volunteerId:Guid}")]
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

        [HttpGet("pet/{petId:Guid}")]
        [SwaggerOperation(Tags = ["Pet"])]
        public async Task<IActionResult> GetPet(
            [FromRoute] GetPetRequest request,
            [FromServices] IQueryHandler<PetDTO?, GetPetQuery> handler,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();
            var result = await handler.Handle(query, cancellationToken);
            return Ok(result.Value);
        }

        [HttpGet("presigned")]
        [SwaggerOperation(Tags = ["Pet"])]
        public async Task<IActionResult> Get(
            [FromQuery] GetPresignedLinkPhotoRequest request,
            [FromServices] GetPresignedLinkPhotoHandler handler,
            CancellationToken cancellationToken = default)
        {
            var getPresignedcommand = GetPresignedLinkPhotoRequest.ToCommand(BUCKET_NAME, request);
            var result = await handler.Handle(getPresignedcommand, cancellationToken);

            return Ok(result);
        }
    }
}
