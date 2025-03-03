using Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

namespace Volunteers.API.Contracts.Volunteers.GetVolunteers;

public record GetFilteredWithPaginationVolunteersRequest(
    string? Name,
    string? Email,
    double? ExperienceInYears,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection)
{
    public GetFilteredWithPaginationVolunteersQuery ToQuery()
        => new GetFilteredWithPaginationVolunteersQuery(Name, Email, ExperienceInYears, Page, PageSize, SortBy, SortDirection);
}