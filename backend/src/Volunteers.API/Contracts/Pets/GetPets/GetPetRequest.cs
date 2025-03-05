using Volunteers.Application.Pets.Queries.GetPet.Queries;

namespace Volunteers.API.Contracts.Pets.GetPets;

public record GetPetRequest(Guid id)
{
    public GetPetQuery ToQuery()
        => new GetPetQuery(id);
}
