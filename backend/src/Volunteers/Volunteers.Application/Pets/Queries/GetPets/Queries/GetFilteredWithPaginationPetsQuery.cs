using Volunteers.Application.Abstractions;
using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.Application.Handlers.Pets.Queries.GetPets.Queries;

public record GetFilteredWithPaginationPetsQuery(
    Guid? VolunteerId,
    string? Name,
    Guid? Species,
    Guid? Breed,
    PetStatus? PetStatus,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection
    ) : IQuery;
