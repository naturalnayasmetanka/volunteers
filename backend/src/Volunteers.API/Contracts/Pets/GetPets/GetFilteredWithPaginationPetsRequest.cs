using Volunteers.Application.Handlers.Pets.Queries.GetPets.Queries;
using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.API.Contracts.Pets.GetPets;

public record GetFilteredWithPaginationPetsRequest(
    string? Name,
    Guid? Species,
    Guid? Breed,
    PetStatus? PetStatus,
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection)
{
    public GetFilteredWithPaginationPetsQuery ToQuery(Guid volunteerId)
        => new GetFilteredWithPaginationPetsQuery(
            VolunteerId: volunteerId, 
            Name: Name, 
            PetStatus: PetStatus, 
            Species: Species, 
            Breed: Breed, 
            Page:Page, 
            PageSize:PageSize,
            SortBy: SortBy,
            SortDirection: SortDirection);
}
