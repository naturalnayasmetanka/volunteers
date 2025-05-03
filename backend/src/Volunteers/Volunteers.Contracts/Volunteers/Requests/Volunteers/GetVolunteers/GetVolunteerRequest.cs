using Volunteers.Application.Volunteers.Queries.GetVolunteer.Queries;

namespace Volunteers.Contracts.Volunteers.Requests.Volunteers.GetVolunteers;

public record GetVolunteerRequest(Guid id)
{
    public GetVolunteerQuery ToQuery()
        => new GetVolunteerQuery(id);
}
