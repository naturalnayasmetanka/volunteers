using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Species.Queries.GetSpecies.Queries;

public record GetSpeciesWithPaginationQuery(
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection) : IQuery;