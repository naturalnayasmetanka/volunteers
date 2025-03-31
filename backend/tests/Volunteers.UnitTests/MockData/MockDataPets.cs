using Volunteers.Domain.PetManagment.Pet.Entities;
using Volunteers.Domain.PetManagment.Pet.Enums;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.UnitTests.MockData;

public class MockDataPets
{
    public Pet GetSinglePet()
    {
        var petId = PetId.NewPetId();
        var nickName = Nickname.Create("keksik").Value;
        var commonDescription = CommonDescription.Create("great keksik").Value;
        var helthDescription = HelthDescription.Create("fine").Value;
        var petPhoneNumber = PetPhoneNumber.Create(123).Value;
        var petStatus = PetStatus.LookingHome;
        var birthDate = DateTime.UtcNow.AddYears(-1);
        var creationDate = DateTime.UtcNow;
        var volunteerId = VolunteerId.Create(Guid.NewGuid());

        var mockPet = Pet.Create(
            id: petId,
            nickname: nickName,
            commonDescription: commonDescription,
            helthDescription: helthDescription,
            phoneNumber: petPhoneNumber,
            helpStatus: petStatus,
            birthDate: birthDate,
            creationDate: creationDate,
            volunteerId: volunteerId,
            speciesBreed: SpeciesBreed.Create(Guid.NewGuid(), Guid.NewGuid()).Value
            );

        return mockPet.Value;
    }

    public List<Pet> GetListPet()
    {
        var pets = Enumerable.Range(1, 5).Select(_ =>
            GetSinglePet());

        return pets.ToList();
    }
}
