using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.PetManagment.Volunteer.AggregateRoot;
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
    public void MovePet_ShouldNotMoveEqualParams_Success()
    {
        MockDataVolunteers volunteers = new MockDataVolunteers();
        var mockVolunteerWithPets = volunteers.GetSingleVolunteerWithPets();

        var positionToSecond = Position.Create(2).Value;

        var firstPet = mockVolunteerWithPets.Pets[0];
        var secondtPet = mockVolunteerWithPets.Pets[1];
        var thirdPet = mockVolunteerWithPets.Pets[2];
        var fourthPet = mockVolunteerWithPets.Pets[3];
        var fifthPet = mockVolunteerWithPets.Pets[4];

        var result = mockVolunteerWithPets
            .MovePetPosition(secondtPet, positionToSecond);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, firstPet.Position.Value);
        Assert.Equal(2, secondtPet.Position.Value);
        Assert.Equal(3, thirdPet.Position.Value);
        Assert.Equal(4, fourthPet.Position.Value);
        Assert.Equal(5, fifthPet.Position.Value);
    }

    [Fact]
    public void MovePet_ShouldMoveForwardDifferentParams_Success()
    {
        MockDataVolunteers volunteers = new MockDataVolunteers();
        var mockVolunteerWithPets = volunteers.GetSingleVolunteerWithPets();

        var toSecond = Position.Create(2).Value;

        var firstPet = mockVolunteerWithPets.Pets[0];
        var secondtPet = mockVolunteerWithPets.Pets[1];
        var thirdPet = mockVolunteerWithPets.Pets[2];
        var fourthPet = mockVolunteerWithPets.Pets[3];
        var fifthPet = mockVolunteerWithPets.Pets[4];

        var result = mockVolunteerWithPets
            .MovePetPosition(fourthPet, toSecond);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, firstPet.Position.Value);
        Assert.Equal(3, secondtPet.Position.Value);
        Assert.Equal(4, thirdPet.Position.Value);
        Assert.Equal(2, fourthPet.Position.Value);
        Assert.Equal(5, fifthPet.Position.Value);
    }

    [Fact]
    public void MovePet_ShouldMoveBackDifferentParams_Success()
    {
        MockDataVolunteers volunteers = new MockDataVolunteers();
        var mockVolunteerWithPets = volunteers.GetSingleVolunteerWithPets();

        var toFourth = Position.Create(4).Value;

        var firstPet = mockVolunteerWithPets.Pets[0];
        var secondtPet = mockVolunteerWithPets.Pets[1];
        var thirdPet = mockVolunteerWithPets.Pets[2];
        var fourthPet = mockVolunteerWithPets.Pets[3];
        var fifthPet = mockVolunteerWithPets.Pets[4];

        var result = mockVolunteerWithPets
            .MovePetPosition(secondtPet, toFourth);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, firstPet.Position.Value);
        Assert.Equal(4, secondtPet.Position.Value);
        Assert.Equal(2, thirdPet.Position.Value);
        Assert.Equal(3, fourthPet.Position.Value);
        Assert.Equal(5, fifthPet.Position.Value);
    }

    [Fact]
    public void MovePet_ShouldMoveFirstToLastParams_Success()
    {
        MockDataVolunteers volunteers = new MockDataVolunteers();
        var mockVolunteerWithPets = volunteers.GetSingleVolunteerWithPets();

        var toFirst = Position.Create(1).Value;

        var firstPet = mockVolunteerWithPets.Pets[0];
        var secondtPet = mockVolunteerWithPets.Pets[1];
        var thirdPet = mockVolunteerWithPets.Pets[2];
        var fourthPet = mockVolunteerWithPets.Pets[3];
        var fifthPet = mockVolunteerWithPets.Pets[4];

        var result = mockVolunteerWithPets
            .MovePetPosition(fifthPet, toFirst);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, firstPet.Position.Value);
        Assert.Equal(3, secondtPet.Position.Value);
        Assert.Equal(4, thirdPet.Position.Value);
        Assert.Equal(5, fourthPet.Position.Value);
        Assert.Equal(1, fifthPet.Position.Value);
    }

    [Fact]
    public void MovePet_ShouldMoveLastToFirstParams_Success()
    {
        MockDataVolunteers volunteers = new MockDataVolunteers();
        var mockVolunteerWithPets = volunteers.GetSingleVolunteerWithPets();

        var toLast = Position.Create(5).Value;

        var firstPet = mockVolunteerWithPets.Pets[0];
        var secondtPet = mockVolunteerWithPets.Pets[1];
        var thirdPet = mockVolunteerWithPets.Pets[2];
        var fourthPet = mockVolunteerWithPets.Pets[3];
        var fifthPet = mockVolunteerWithPets.Pets[4];

        var result = mockVolunteerWithPets
            .MovePetPosition(firstPet, toLast);

        Assert.True(result.IsSuccess);
        Assert.Equal(5, firstPet.Position.Value);
        Assert.Equal(1, secondtPet.Position.Value);
        Assert.Equal(2, thirdPet.Position.Value);
        Assert.Equal(3, fourthPet.Position.Value);
        Assert.Equal(4, fifthPet.Position.Value);
    }
}