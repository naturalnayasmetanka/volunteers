using CSharpFunctionalExtensions;
using Dapper;
using System.Text;
using System.Text.Json;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Database;
using Volunteers.Application.DTO;
using Volunteers.Application.Extentions;
using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteers.Queries;
using Volunteers.Application.Models;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteers;

public class GetPaginateVolunteersHandler : IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery>
{
    private readonly ISqlConnConnectionFactory _connection;
    public GetPaginateVolunteersHandler(ISqlConnConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<PagedList<VolunteerDTO>, Error>> Handle(
       GetFilteredWithPaginationVolunteersQuery query,
       CancellationToken cancellationToken = default)
    {
        var connection = _connection.Create();

        var parameters = new DynamicParameters();

        var sqlTotalCountQuery = new StringBuilder("""
                            SELECT COUNT(*) FROM volunteers
                        """
                      );

        sqlTotalCountQuery.ApplyFilterByColumn(columnName: "name", queryFieldValue: query.Name);
        sqlTotalCountQuery.ApplyFilterByColumn(columnName: "email", queryFieldValue: query.Email);
        sqlTotalCountQuery.ApplyFilterByColumn(columnName: "experience_in_years", queryFieldValue: query.ExperienceInYears);

        var totalCount = await connection.ExecuteScalarAsync<long>(sqlTotalCountQuery.ToString());

        var sqlQuery = new StringBuilder("""
                           SELECT id, name, email, experience_in_years, phone_number, "RequisiteDetails", "SocialNetworkDetails"
                           FROM volunteers
                        """);

        sqlQuery.ApplyFilterByColumn(columnName: "name", queryFieldValue: query.Name);
        sqlQuery.ApplyFilterByColumn(columnName: "email", queryFieldValue: query.Email);
        sqlQuery.ApplyFilterByColumn(columnName: "experience_in_years", queryFieldValue: query.ExperienceInYears);

        sqlQuery.ApplySorting(sortBy: query.SortBy, sortDirection: query.SortDirection, dynamicParameters: parameters);

        sqlQuery.ApplyPagination(page: query.Page, pageSize: query.PageSize, dynamicParameters: parameters);

        var volunteers = await connection.QueryAsync<VolunteerDTO, string?, string?, VolunteerDTO>(
            sqlQuery.ToString(),
            (volunteer, jsonRequisites, jsonSotials) =>
            {
                var requisites = jsonRequisites is null ? null : JsonSerializer.Deserialize<RequisiteDetailsDTO>(jsonRequisites);
                var sotials = jsonSotials is null ? null : JsonSerializer.Deserialize<SocialNetworkDetailsDTO>(jsonSotials);

                volunteer.RequisiteDetails = requisites;
                volunteer.SocialNetworkDetails = sotials;

                return volunteer;
            },
            splitOn: "RequisiteDetails, SocialNetworkDetails",
            param: parameters);

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