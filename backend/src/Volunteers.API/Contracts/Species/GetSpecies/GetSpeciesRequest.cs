using Volunteers.Application.Handlers.Species.Queries.GetSpecies.Queries;

namespace Volunteers.API.Contracts.Species.GetSpecies
{
    public record GetSpeciesRequest(
        int Page,
        int PageSize,
        string? SortBy,
        string? SortDirection)
    {
        public GetSpeciesWithPaginationQuery ToQuery()
            => new GetSpeciesWithPaginationQuery(Page, PageSize, SortBy, SortDirection);
    }
}