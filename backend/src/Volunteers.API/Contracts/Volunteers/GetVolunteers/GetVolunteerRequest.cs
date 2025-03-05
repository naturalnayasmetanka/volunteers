using Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteer.Queries;

namespace Volunteers.API.Contracts.Volunteers.GetVolunteers;

public record GetVolunteerRequest(Guid id)
{
    public GetVolunteerQuery ToQuery()
        => new GetVolunteerQuery(id);
}
