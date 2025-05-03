using Microsoft.EntityFrameworkCore;
using Shared.Kernel.Ids;
using Volunteers.Application.Volunteers;
using Volunteers.Domain.Volunteers.AggregateRoot;
using Volunteers.Infrastructure.Contexts;

namespace Volunteers.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly VolunteersDbContext _context;

    public VolunteerRepository(VolunteersDbContext context)
    {
        _context = context;
    }

    public async Task<Volunteer> CreateAsync(
        Volunteer newVolunteer,
        CancellationToken cancellationToken = default)
    {
        await _context.Volunteers.AddAsync(newVolunteer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newVolunteer;
    }

    public async Task<Volunteer?> GetByIdAsync(
        VolunteerId id,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _context.Volunteers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return volunteer;
    }

    public async Task<Guid> UpdateAsync(
        Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        _context.Volunteers.Attach(volunteer);
        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public void Attach(Volunteer volunteer)
    {
        _context.Volunteers.Attach(volunteer);
    }

    public async Task<Guid> DeleteAsync(
        Volunteer volunteer,
        CancellationToken cancellationToken = default)
    {
        _context.Volunteers.Remove(volunteer);
        await _context.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}