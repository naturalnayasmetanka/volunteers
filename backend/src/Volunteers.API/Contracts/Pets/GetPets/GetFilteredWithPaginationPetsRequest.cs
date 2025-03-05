using Volunteers.Application.Handlers.Pets.Queries.GetPets.Queries;

namespace Volunteers.API.Contracts.Pets.GetPets;

public record GetFilteredWithPaginationPetsRequest(
    Guid? VolunteerId,
    string? Name,
    int? Age,
    string? Species,
    string? Breed)
{
    public GetFilteredWithPaginationPetsQuery ToQuery()
        => new GetFilteredWithPaginationPetsQuery(VolunteerId, Name, Age, Species, Breed);
}
