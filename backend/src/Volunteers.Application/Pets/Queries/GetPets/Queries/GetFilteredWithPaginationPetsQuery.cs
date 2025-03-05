﻿using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Pets.Queries.GetPets.Queries;

public record GetFilteredWithPaginationPetsQuery(
    Guid? VolunteerId,
    string? Name,
    int? Age,
    string? Species,
    string? Breed
    ) : IQuery;
