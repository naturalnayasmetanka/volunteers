﻿using CSharpFunctionalExtensions;
using Volunteers.Domain.Pet.Enums;
using CustomEntity = Volunteers.Domain.Shared;
using PetModel = Volunteers.Domain.Pet.Models.Pet;

namespace Volunteers.Domain.Volunteer.Models;

public class Volunteer : CustomEntity.Models.Entity<VolunteerId>
{
    private Volunteer(VolunteerId id)
        : base(id)
    {

    }

    private Volunteer(
        VolunteerId id,
        Name name,
        Email email,
        ExperienceInYears experienceInYears,
        PhoneNumber phoneNumber
        ) : base(id)
    {
        Name = name;
        Email = email;
        ExperienceInYears = experienceInYears;
        PhoneNumber = phoneNumber;
    }

    public Name Name { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public ExperienceInYears ExperienceInYears { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;

    private List<SocialNetwork> _socialNetworks = [];
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;

    private List<VolunteerRequisite> _requisites = [];
    public IReadOnlyList<VolunteerRequisite> Requisites => _requisites;

    private List<PetModel> _pets = [];
    public IReadOnlyList<PetModel> Pets => _pets;

    public static Result<Volunteer> Create(
        VolunteerId id,
        Name name,
        Email email,
        ExperienceInYears experienceInYears,
        PhoneNumber phoneNumber)
    {
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
        _socialNetworks.Add(socialNetwork);
    }

    public void AddVolunteerRequisite(VolunteerRequisite requisite)
    {
        _requisites.Add(requisite);
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