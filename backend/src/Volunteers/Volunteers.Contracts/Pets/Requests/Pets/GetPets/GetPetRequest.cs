

using Volunteers.Application.Pets.Queries.GetPet.Queries;

namespace Volunteers.Contracts.Pets.Requests.Pets.GetPets;

public record GetPetRequest(Guid petId)
{
    public GetPetQuery ToQuery()
        => new GetPetQuery(Id: petId);
}
