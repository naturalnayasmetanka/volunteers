using CSharpFunctionalExtensions;
using Volunteers.Domain.Pet.Enums;
using CustomEntity = Volunteers.Domain.Shared;

namespace Volunteers.Domain.Pet.Models;

public class Pet : CustomEntity.Entity<PetId>
{
    private Pet(PetId id)
        :base(id)
    {
        
    }

    private Pet(
        PetId id,
        string nickname,
        string commonDescription,
        string helthDescription,
        int phoneNumber,
        PetStatus helpStatus,
        DateTime birthDate,
        DateTime creationDate
        ) : base(id)
    {
        Nickname = nickname;
        CommonDescription = commonDescription;
        HelthDescription = helthDescription;
        PhoneNumber = phoneNumber;
        HelpStatus = helpStatus;
        BirthDate = birthDate;
        CreationDate = creationDate;
    }

    public string Nickname { get; private set; } = default!;
    public string CommonDescription { get; private set; } = default!;
    public string HelthDescription { get; private set; } = default!;
    public int PhoneNumber { get; private set; }
    public PetStatus HelpStatus { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public DateTime CreationDate { get; private set; }

    public PetDetailsLocation? LocationDetails { get; private set; }
    public PetDetailsRequisite? RequisiteDetails { get; private set; }
    public PetDetailsPhoto? PhotoDetails { get; private set; }
    public PetDetailsPhysicalParameters? PhysicalDetails { get; private set; }
    public SpeciesBreed? SpeciesBreed { get; set; }

    public static Result<Pet> Create(
        PetId id,
        string nickname,
        string commonDescription,
        string helthDescription,
        int phoneNumber,
        PetStatus helpStatus,
        DateTime birthDate,
        DateTime creationDate
    )
    {
        if (string.IsNullOrWhiteSpace(nickname))
            return Result.Failure<Pet>("Nickname can not be empty");

        if (string.IsNullOrWhiteSpace(commonDescription))
            return Result.Failure<Pet>("Common Description can not be empty");

        if (string.IsNullOrWhiteSpace(helthDescription))
            return Result.Failure<Pet>("Helth Description can not be empty");

        if (phoneNumber <= 0)
            return Result.Failure<Pet>("Phone number cannot be less than or equal to zero");

        var newPet = new Pet(
            id: id,
            nickname: nickname,
            commonDescription: commonDescription,
            helthDescription: helthDescription,
            phoneNumber: phoneNumber,
            helpStatus: helpStatus,
            birthDate: birthDate,
            creationDate: creationDate);

        return Result.Success(newPet);
    }

    public void AddLocation(Location location)
    {
        LocationDetails?.Locations.Add(location);
    }

    public void AddRequisite(PetRequisite requisite)
    {
        RequisiteDetails?.Requisites.Add(requisite);
    }

    public void AddPhysicalParameters(PhysicalParameters physicalParameters)
    {
        PhysicalDetails?.PhysicalParameters.Add(physicalParameters);
    }

    public void AddPhoto(PetPhoto photo)
    {
        PhotoDetails?.Photo.Add(photo);
    }
}