using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.UnitTests.MockData;
using Xunit;

namespace Volunteers.UnitTests.Domain;

public class VolunteerTests
{
    [Fact]
    public void AddPet_FirstCorrectPet_Success()
    {
        MockDataVolunteers volunteers = new MockDataVolunteers();
        MockDataPets pets = new MockDataPets();

        var mockVolunteer = volunteers.GetSingleVolunteer();
        var mockPet = pets.GetSinglePet();

        var result = mockVolunteer.AddPet(mockPet);

        var serialNumber = mockPet.Position.Value;

        Assert.True(result.IsSuccess);
        Assert.Equal(serialNumber, Position.First.Value);
    }

    [Fact]
    public void AddPet_CorrectPet_Success()
    {
        MockDataVolunteers volunteers = new MockDataVolunteers();
        MockDataPets pets = new MockDataPets();

        var mockVolunteer = volunteers.GetSingleVolunteer();
        var firstPet = pets.GetSinglePet();
        var secondPet = pets.GetSinglePet();

        var firstPetResult = mockVolunteer.AddPet(firstPet);
        var secondPetResult = mockVolunteer.AddPet(secondPet);

        var secondSerialNumber = secondPet.Position.Value;

        Assert.True(firstPetResult.IsSuccess);
        Assert.True(secondPetResult.IsSuccess);
        Assert.Equal(secondSerialNumber, Position.First.Value + 1);
    }

    [Fact]
    public void MovePet_ShouldNotMove_()
    {

        //var result = Volunteer
    }
}
