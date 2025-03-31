﻿using System.Text.Json;
using CSharpFunctionalExtensions;
using Dapper;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Database;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteer.Queries;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteer;

public class GetVolunteerHandler : IQueryHandler<VolunteerDTO?, GetVolunteerQuery>
{
    private readonly ISqlConnConnectionFactory _connection;
    public GetVolunteerHandler(ISqlConnConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<VolunteerDTO?, Error>> Handle(
       GetVolunteerQuery query,
       CancellationToken cancellationToken = default)
    {
        var connection = _connection.Create();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", query.Id);

        var sqlQuery = """
                        SELECT id, name, email, experience_in_years, phone_number, "RequisiteDetails", "SocialNetworkDetails"
                        FROM volunteers
                        WHERE Id = @Id
                        """;

        var volunteer = await connection.QueryAsync<VolunteerDTO, string?, string?, VolunteerDTO>(
            sqlQuery,
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

        return volunteer.FirstOrDefault();
    }
}
