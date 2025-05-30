﻿using System.Text;
using CSharpFunctionalExtensions;
using Dapper;
using Shared.Core.Abstractions.Database;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Core.Extentions;
using Shared.Core.Models;
using Shared.Kernel.CustomErrors;
using Species.Application.Species.Handlers.Queries.GetSpecies.Queries;

namespace Species.Application.Species.Handlers.Queries.GetSpecies;

public class GetSpeciesHandler : IQueryHandler<PagedList<SpeciesDTO>, GetSpeciesWithPaginationQuery>
{
    private readonly ISqlConnConnectionFactory _connection;
    public GetSpeciesHandler(ISqlConnConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<PagedList<SpeciesDTO>, Error>> Handle(
       GetSpeciesWithPaginationQuery query,
       CancellationToken cancellationToken = default)
    {
        var connection = _connection.Create();
        var parameters = new DynamicParameters();

        var sqlTotalCountQuery = new StringBuilder("""
                        SELECT COUNT(*) FROM species
                    """
                  );

        var totalCount = await connection.ExecuteScalarAsync<long>(sqlTotalCountQuery.ToString());

        var sqlQuery = new StringBuilder("""
                       SELECT id, title, description
                       FROM species
                    """);

        sqlQuery.ApplySorting(sortBy: query.SortBy, sortDirection: query.SortDirection, dynamicParameters: parameters);
        sqlQuery.ApplyPagination(page: query.Page, pageSize: query.PageSize, dynamicParameters: parameters);

        var species = await connection.QueryAsync<SpeciesDTO>(sqlQuery.ToString());

        var pagedList = new PagedList<SpeciesDTO>()
        {
            Items = species.ToList(),
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount
        };

        return pagedList;
    }
}
