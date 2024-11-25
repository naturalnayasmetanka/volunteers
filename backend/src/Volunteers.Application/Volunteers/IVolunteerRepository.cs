﻿using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;

namespace Volunteers.Application.Volunteer;

public interface IVolunteerRepository
{
    public Task<VolunteerModel> CreateAsync(VolunteerModel newVolunteer, CancellationToken cancellationToken = default);
    public Task<VolunteerModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}