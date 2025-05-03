using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Framework;
using Species.Application.Breeds.Handlers.Queries.GetBreed.Queries;
using Species.Contracts.Breeds.Requests.GetBreeds;
using Swashbuckle.AspNetCore.Annotations;

namespace Species.Presentation.Controllers;

public class BreedController : ApplicationController
{
    [HttpGet("breed")]
    [SwaggerOperation(Tags = ["Breed"])]
    public async Task<IActionResult> GetBreed(
        [FromServices] IQueryHandler<BreedDTO, GetBreedQuery> handler,
        [FromQuery] GetBreedRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result);
    }
}
