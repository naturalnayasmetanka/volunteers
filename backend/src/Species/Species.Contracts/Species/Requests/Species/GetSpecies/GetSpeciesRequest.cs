using Species.Application.Species.Handlers.Queries.GetSpecies.Queries;

namespace Species.Contracts.Species.Requests.Species.GetSpecies
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