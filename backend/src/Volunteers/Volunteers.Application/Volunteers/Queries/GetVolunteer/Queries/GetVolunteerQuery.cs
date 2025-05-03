using Shared.Core.Abstractions.Handlers;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteer.Queries;

public record GetVolunteerQuery(Guid Id) : IQuery;
