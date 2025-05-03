using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.Enums;

namespace Volunteers.Application.Pets.Queries.GetPets.Queries;

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
