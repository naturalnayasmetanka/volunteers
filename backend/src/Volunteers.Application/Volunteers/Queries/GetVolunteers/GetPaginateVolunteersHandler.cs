using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;
using Volunteers.Application.Models;
using Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteers;

public class GetPaginateVolunteersHandler : IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery>
{
    public async Task<PagedList<VolunteerDTO>> Handle(
        GetFilteredWithPaginationVolunteersQuery query,
        CancellationToken cancellationToken = default)
    {
        return null;
    }
}