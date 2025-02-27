using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

public record GetFilteredWithPaginationVolunteersQuery(
    string? Name,
    string? Email,
    double? ExperienceInYears,
    int Page,
    int PageSize) : IQuery;

