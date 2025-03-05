using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volunteers.API.Contracts.Breeds.GetBreeds;
using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Breeds.Queries.GetBreed.Queries;

namespace Volunteers.API.Controllers;

[Route("api/breeds")]
[ApiController]
public class BreedsController : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Tags = ["Breed"])]
    public async Task<IActionResult> Get(
        [FromServices] IQueryHandler<BreedDTO, GetBreedQuery> handler,
        [FromQuery] GetBreedRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result);
    }
}