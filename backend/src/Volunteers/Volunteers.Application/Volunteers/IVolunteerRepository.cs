using Shared.Kernel.Ids;
using VolunteerModel = Volunteers.Domain.Volunteers.AggregateRoot.Volunteer;

namespace Volunteers.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<VolunteerModel> CreateAsync(VolunteerModel newVolunteer, CancellationToken cancellationToken = default);
    Task<VolunteerModel?> GetByIdAsync(VolunteerId id, CancellationToken cancellationToken = default);
    Task<Guid> UpdateAsync(VolunteerModel volunteer, CancellationToken cancellationToken = default);
    Task<Guid> DeleteAsync(VolunteerModel volunteer, CancellationToken cancellationToken = default);
    Task SaveAsync(CancellationToken cancellationToken = default);
    void Attach(VolunteerModel volunteer);
}
