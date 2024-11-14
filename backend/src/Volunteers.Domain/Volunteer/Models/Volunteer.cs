using CSharpFunctionalExtensions;
using Volunteers.Domain.Pet.Enums;
using CustomEntity = Volunteers.Domain.Shared;
using PetModel = Volunteers.Domain.Pet.Models.Pet;

namespace Volunteers.Domain.Volunteer.Models;

public class Volunteer : CustomEntity.Entity<VolunteerId>
{
    private Volunteer(VolunteerId id)
        :base(id)
    {
        
    }

    private Volunteer(
        VolunteerId id,
        string name,
        string email,
        double experienceInYears,
        int phoneNumber
        ) : base(id)
    {
        Name = name;
        Email = email;
        ExperienceInYears = experienceInYears;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public double ExperienceInYears { get; private set; }
    public int PhoneNumber { get; private set; }

    public SocialNetworkDetails? SocialNetworkDetails { get; private set; }
    public VolunteerRequisiteDetails? RequisiteDetails { get; private set; }

    private List<PetModel> _pets = [];
    public IReadOnlyList<PetModel> Pets => _pets;

    public static Result<Volunteer> Create(
        VolunteerId id,
        string name,
        string email,
        double experienceInYears,
        int phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Volunteer>("Name can not be empty");

        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Volunteer>("Email can not be empty");

        if (experienceInYears < 0)
            return Result.Failure<Volunteer>("Experience in years cannot be less than to zero");

        if (phoneNumber <= 0)
            return Result.Failure<Volunteer>("Phone number cannot be less than or equal to zero");

        var newVolunteer = new Volunteer(
            id: id,
            name: name,
            email: email,
            experienceInYears: experienceInYears,
            phoneNumber: phoneNumber);

        return Result.Success(newVolunteer);
    }

    public void AddSocialNetwork(SocialNetwork socialNetwork)
    {
        SocialNetworkDetails?.SocialNetworks.Add(socialNetwork);
    }

    public void AddVolunteerRequisite(VolunteerRequisite requisite)
    {
        RequisiteDetails?.Requisites.Add(requisite);
    }

    public void AddPet(PetModel pet)
    {
        _pets.Add(pet);
    }

    public int NumberOfAttachedAnimals()
        => Pets.Where(x => x.HelpStatus == PetStatus.FoundHome).Count();

    public int NumberOfLookingHomeAnimals()
        => Pets.Where(x => x.HelpStatus == PetStatus.LookingHome).Count();

    public int NumberOfNeedsHelpAnimals()
        => Pets.Where(x => x.HelpStatus == PetStatus.NeedsHelp).Count();
}