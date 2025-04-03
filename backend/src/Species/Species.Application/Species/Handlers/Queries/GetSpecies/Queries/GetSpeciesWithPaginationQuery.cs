using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Species.Application.Species.Handlers.Queries.GetSpecies.Queries;

public record GetSpeciesWithPaginationQuery(
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection) : IQuery;