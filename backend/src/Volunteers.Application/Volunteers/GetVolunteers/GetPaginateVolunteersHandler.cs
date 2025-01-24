using Volunteers.Application.Models;
using Volunteers.Application.Volunteers.GetVolunteers.Queries;

namespace Volunteers.Application.Volunteers.GetVolunteers;

public class GetPaginateVolunteersHandler
{
    public async Task Handle(
        GetPaginateVolunteersQuery command,
        CancellationToken cancellationToken = default)
    {

    }
}