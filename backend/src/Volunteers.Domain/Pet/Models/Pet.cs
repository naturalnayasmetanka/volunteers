using CSharpFunctionalExtensions;
using Volunteers.Domain.Pet.Enums;
using CustomEntity = Volunteers.Domain.Shared;
using VolunteerModel = Volunteers.Domain.Volunteer.Models.Volunteer;

namespace Volunteers.Domain.Pet.Models;

public class Pet : CustomEntity.Models.Entity<PetId>
{
    private Pet(PetId id)
        :base(id)
    {
        
    }

    private Pet(
        PetId id,
        Nickname nickname,
        CommonDescription commonDescription,
        HelthDescription helthDescription,
        PetPhoneNumber phoneNumber,
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

    public Nickname Nickname { get; private set; } = default!;
    public CommonDescription CommonDescription { get; private set; } = default!;
    public HelthDescription HelthDescription { get; private set; } = default!;
    public PetPhoneNumber PhoneNumber { get; private set; } = default!;
    public PetStatus HelpStatus { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public DateTime CreationDate { get; private set; }


    public VolunteerModel Volunteer { get; private set; } = default!;


    private List<Location> _locations = [];
    public IReadOnlyList<Location> Locations => _locations;
   
    private List<PetRequisite> _requisites = [];
    public IReadOnlyList<PetRequisite> Requisites => _requisites;

    private List<PetPhoto> _photo = [];
    public IReadOnlyList<PetPhoto> Photo => _photo;

    private List<PhysicalParameters> _physicalParameters = [];
    public IReadOnlyList<PhysicalParameters> PhysicalParameters => _physicalParameters;

    public SpeciesBreed? SpeciesBreed { get; private set; } = default!;

    public static Result<Pet> Create(
        PetId id,
        Nickname nickname,
        CommonDescription commonDescription,
        HelthDescription helthDescription,
        PetPhoneNumber phoneNumber,
        PetStatus helpStatus,
        DateTime birthDate,
        DateTime creationDate
    )
    {
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
        _locations.Add(location);
    }

    public void AddRequisite(PetRequisite requisite)
    {
        _requisites.Add(requisite);
    }

    public void AddPhysicalParameters(PhysicalParameters physicalParameters)
    {
        _physicalParameters.Add(physicalParameters);
    }

    public void AddPhoto(PetPhoto photo)
    {
        _photo.Add(photo);
    }
}