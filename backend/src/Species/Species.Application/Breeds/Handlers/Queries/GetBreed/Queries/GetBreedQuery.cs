using Shared.Core.Abstractions.Handlers;

namespace Species.Application.Breeds.Handlers.Queries.GetBreed.Queries;

public record GetBreedQuery(int Page, int PageSize, Guid SpeciesId) : IQuery;