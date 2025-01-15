using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volunteers.API.Contracts.Volunteers.GetPresignedLinkPhoto;
using Volunteers.Application.Volunteers.GetPresignedLinkPhoto;

namespace Volunteers.API.Controllers
{
    [Route("api/pet")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private const string BUCKET_NAME = "photos";

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
