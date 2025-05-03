

using Species.Application.Breeds.Handlers.Queries.GetBreed.Queries;

namespace Species.Contracts.Breeds.Requests.GetBreeds;

public record GetBreedRequest(int Page, int PageSize, Guid SpeciesId)
{
    public GetBreedQuery ToQuery()
        => new GetBreedQuery(Page, PageSize, SpeciesId);
}
