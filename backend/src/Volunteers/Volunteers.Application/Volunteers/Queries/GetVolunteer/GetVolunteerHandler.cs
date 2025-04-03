using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteer;

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
