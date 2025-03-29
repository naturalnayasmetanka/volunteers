using Volunteers.Application.Handlers.Pets.Queries.GetPet.Queries;

namespace Volunteers.API.Contracts.Pets.GetPets;

public record GetPetRequest(Guid petId)
{
    public GetPetQuery ToQuery()
        => new GetPetQuery(Id: petId);
}
