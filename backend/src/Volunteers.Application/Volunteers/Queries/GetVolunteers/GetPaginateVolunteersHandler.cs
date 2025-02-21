using Volunteers.Application.Models;
using Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

namespace Volunteers.Application.Volunteers.Queries.GetVolunteers;

public class GetPaginateVolunteersHandler
{
    public async Task Handle(
        GetPaginateVolunteersQuery command,
        CancellationToken cancellationToken = default)
    {

    }
}