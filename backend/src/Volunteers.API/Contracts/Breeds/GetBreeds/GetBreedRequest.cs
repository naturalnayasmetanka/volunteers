using Volunteers.Application.Handlers.Breeds.Queries.GetBreed.Queries;

namespace Volunteers.API.Contracts.Breeds.GetBreeds;

public record GetBreedRequest(int Page, int PageSize, Guid SpeciesId)
{
    public GetBreedQuery ToQuery()
        => new GetBreedQuery(Page, PageSize, SpeciesId);
}
