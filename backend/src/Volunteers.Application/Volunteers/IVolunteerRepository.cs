using Volunteers.Domain.Shared.Ids;
using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;

namespace Volunteers.Application.Volunteer;

public interface IVolunteerRepository
{
    Task<VolunteerModel> CreateAsync(VolunteerModel newVolunteer, CancellationToken cancellationToken = default);
    Task<VolunteerModel?> GetByIdAsync(VolunteerId id, CancellationToken cancellationToken = default);
    Task<Guid> UpdateAsync(VolunteerModel volunteer, CancellationToken cancellationToken = default);
    Task<Guid> DeleteAsync(VolunteerModel volunteer, CancellationToken cancellationToken = default);
    Task SaveAsync(CancellationToken cancellationToken = default);
    void Attach(VolunteerModel volunteer);
}