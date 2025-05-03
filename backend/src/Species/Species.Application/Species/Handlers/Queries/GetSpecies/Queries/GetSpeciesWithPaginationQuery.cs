using Shared.Core.Abstractions.Handlers;

namespace Species.Application.Species.Handlers.Queries.GetSpecies.Queries;

public record GetSpeciesWithPaginationQuery(
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection) : IQuery;
