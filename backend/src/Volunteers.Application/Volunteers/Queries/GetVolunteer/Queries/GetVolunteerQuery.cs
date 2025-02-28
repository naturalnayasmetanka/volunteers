using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteer.Queries;

public record GetVolunteerQuery(Guid Id) : IQuery;