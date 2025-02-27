using Dapper;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Database;
using Volunteers.Application.DTO;
using Volunteers.Application.Models;
using Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteers;

public class GetPaginateVolunteersHandler : IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery>
{
    private readonly ISqlConnConnectionFactory _connection;
    public GetPaginateVolunteersHandler(ISqlConnConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<PagedList<VolunteerDTO>> Handle(
        GetFilteredWithPaginationVolunteersQuery query,
        CancellationToken cancellationToken = default)
    {
        var connection = _connection.Create();

        var parameters = new DynamicParameters();
        parameters.Add("@PageSize", query.PageSize);
        parameters.Add("@Offset", (query.Page - 1) * query.PageSize);


        var sqlQuery = """
                           SELECT id, name, email, experience_in_years, phone_number, "RequisiteDetails", "SocialNetworkDetails"
                           FROM volunteers
                           LIMIT @PageSize OFFSET @Offset
                        """;

        var totalCount = await connection.ExecuteScalarAsync<long>("SELECT COUNT(*) FROM volunteers");
        var volunteers = await connection.QueryAsync<VolunteerDTO>(sqlQuery, parameters);

      
        var pagedList = new PagedList<VolunteerDTO>()
        {
            Items = volunteers.ToList(),
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount
        };

        return pagedList;
    }
}