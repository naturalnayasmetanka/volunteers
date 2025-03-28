using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volunteers.API.Contracts.Pets.GetPets;
using Volunteers.API.Contracts.Volunteers.GetPresignedLinkPhoto;
using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Pets.Queries.GetPet.Queries;
using Volunteers.Application.Handlers.Volunteers.Commands.GetPresignedLinkPhoto;

namespace Volunteers.API.Controllers
{
    [Route("api/pet")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private const string BUCKET_NAME = "photos";

        [HttpGet]
        [SwaggerOperation(Tags = ["Pet"])]
        public async Task<IActionResult> GetPets(
            [FromServices]
            [FromQuery] GetFilteredWithPaginationPetsRequest request,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();

            return Ok();
        }

        [HttpGet("{Id:Guid}")]
        [SwaggerOperation(Tags = ["Pet"])]
        public async Task<IActionResult> GetPet(
            [FromRoute] GetPetRequest request,
            [FromServices] IQueryHandler<PetDTO?, GetPetQuery> handler,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();
            var result = await handler.Handle(query, cancellationToken); // не приходят некторые поля
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
