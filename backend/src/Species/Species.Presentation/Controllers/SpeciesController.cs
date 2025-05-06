using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Core.Models;
using Shared.Framework;
using Species.Application.Species.Handlers.Queries.GetSpecies.Queries;
using Species.Contracts.Species.Requests.Species.GetSpecies;
using Swashbuckle.AspNetCore.Annotations;

namespace Species.Presentation.Controllers;

public class SpeciesController : ApplicationController
{
    [HttpGet("species")]
    [SwaggerOperation(Tags = ["Species"])]
    public async Task<IActionResult> GetSpecies(
        [FromServices] IQueryHandler<PagedList<SpeciesDTO>, GetSpeciesWithPaginationQuery> handler,
        [FromQuery] GetSpeciesRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result);
    }
}
