using CSharpFunctionalExtensions;
using Volunteers.Domain.PetManagment.Pet.Enums;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared;
using Volunteers.Domain.Shared.Ids;
using CustomEntity = Volunteers.Domain.Shared;
using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;

namespace Volunteers.Domain.PetManagment.Pet.Entities;

public class Pet : CustomEntity.Entity<PetId>, ISoftDeletable
{
    private bool _IsDelete = false;

    private List<Location> _locations = [];
    private List<PetRequisite> _requisites = [];
    private List<PhysicalParameters> _physicalParameters = [];

    private List<PetPhoto> _photo = [];
    private Pet(PetId id) : base(id) { }

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

    public IReadOnlyList<Location> Locations => _locations;
    public IReadOnlyList<PetRequisite> Requisites => _requisites;
    public IReadOnlyList<PetPhoto> Photo => _photo;
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

    public void SoftDelete()
    {
        _IsDelete = true;
    }

    public void Restore()
    {
        _IsDelete = false;
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