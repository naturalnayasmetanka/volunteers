﻿using Microsoft.EntityFrameworkCore;
using Volunteers.Application.Volunteer;
using Volunteers.Domain.PetManagment.Volunteer.AggregateRoot;
using Volunteers.Infrastructure.Contexts;

namespace Volunteers.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _context;

    public VolunteerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Volunteer> CreateAsync(Volunteer newVolunteer, CancellationToken cancellationToken = default)
    {
        await _context.Volunteers.AddAsync(newVolunteer, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return newVolunteer;
    }

    public async Task<Volunteer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.Id == id);

        return volunteer;
    }
}