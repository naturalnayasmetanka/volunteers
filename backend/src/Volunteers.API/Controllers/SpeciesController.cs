using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Volunteers.API.Contracts.Species.GetSpecies;
using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Species.Queries.GetSpecies.Queries;
using Volunteers.Application.Models;

namespace Volunteers.API.Controllers;

[Route("api/species")]
[ApiController]
public class SpeciesController : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Tags = ["Species"])]
    public async Task<IActionResult> Get(
        [FromServices] IQueryHandler<PagedList<SpeciesDTO>, GetSpeciesWithPaginationQuery> handler,
        [FromQuery] GetSpeciesRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        var result = await handler.Handle(query, cancellationToken);

        return Ok(result);
    }
}