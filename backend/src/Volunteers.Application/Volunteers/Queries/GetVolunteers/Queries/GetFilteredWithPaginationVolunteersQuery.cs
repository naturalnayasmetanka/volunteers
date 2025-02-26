using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

public class GetFilteredWithPaginationVolunteersQuery(string? Title, int Page, int PageSize) : IQuery;
