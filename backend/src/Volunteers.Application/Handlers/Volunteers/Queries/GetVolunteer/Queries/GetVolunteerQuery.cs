using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Volunteers.Queries.GetVolunteer.Queries;

public record GetVolunteerQuery(Guid Id) : IQuery;