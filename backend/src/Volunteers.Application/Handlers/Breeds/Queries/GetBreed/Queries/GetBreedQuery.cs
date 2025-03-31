using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Breeds.Queries.GetBreed.Queries;

public record GetBreedQuery(int Page, int PageSize, Guid SpeciesId) : IQuery;