using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteers.Application.Handlers.Breeds.Queries.GetBreed.Queries;

namespace Species.Application.Breeds.Handlers.Queries.GetBreed;

public class GetBreedHandler : IQueryHandler<PagedList<BreedDTO>, GetBreedQuery>
{
    private readonly ISqlConnConnectionFactory _connection;
    public GetBreedHandler(ISqlConnConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<PagedList<BreedDTO>, Error>> Handle(
       GetBreedQuery query,
       CancellationToken cancellationToken = default)
    {
        var connection = _connection.Create();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", query.SpeciesId);

        var sqlTotalCountQuery = new StringBuilder("""
                            SELECT COUNT(*) FROM breeds
                        """
                      );

        var totalCount = await connection.ExecuteScalarAsync<long>(sqlTotalCountQuery.ToString());

        var sqlQuery = new StringBuilder("""
                           SELECT id, title, description
                           FROM breeds
                           WHERE species_id = @Id
                        """);

        sqlQuery.ApplyPagination(page: query.Page, pageSize: query.PageSize, dynamicParameters: parameters);

        var breeds = await connection.QueryAsync<BreedDTO>(sqlQuery.ToString());

        var pagedList = new PagedList<BreedDTO>()
        {
            Items = breeds.ToList(),
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount
        };

        return pagedList;
    }
}