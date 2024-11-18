using VolunteerModel = Volunteers.Domain.Volunteer.Models.Volunteer;

namespace Volunteers.Application.Volunteer;

public interface IVolunteerRepository
{
    public Task<VolunteerModel> CreateAsync(VolunteerModel newVolunteer, CancellationToken cancellationToken = default);
}