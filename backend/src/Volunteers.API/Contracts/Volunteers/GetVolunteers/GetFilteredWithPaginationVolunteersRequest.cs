using Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

namespace Volunteers.API.Contracts.Volunteers.GetVolunteers;

public record GetFilteredWithPaginationVolunteersRequest(string? Title,int Page, int PageSize)
{
    public GetFilteredWithPaginationVolunteersQuery ToQuery() 
        => new GetFilteredWithPaginationVolunteersQuery(Title, Page, PageSize);
}