﻿using CSharpFunctionalExtensions;
using Volunteers.Domain.PetManagment.Pet.Enums;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared;
using Volunteers.Domain.Shared.Ids;
using CustomEntity = Volunteers.Domain.Shared;
using PetModel = Volunteers.Domain.PetManagment.Pet.Entities.Pet;

namespace Volunteers.Domain.PetManagment.Volunteer.AggregateRoot;

public class Volunteer : CustomEntity.Entity<VolunteerId>, ISoftDeletable
{
    private bool _isDeleted = false;
    private List<PetModel> _pets = [];

    private Volunteer(VolunteerId id) : base(id) { }

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

    public SocialNetworkDetails? SocialNetworkDetails { get; private set; }
    public RequisiteDetails? RequisiteDetails { get; private set; }
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

    public void SoftDelete()
    {
        _isDeleted = true;
    }

    public void Restore()
    {
        _isDeleted = false;
    }

    public void UpdateMainInfo(
        Name name,
        Email email,
        ExperienceInYears experienceInYears,
        PhoneNumber phoneNumber)
    {
        Name = name;
        Email = email;
        ExperienceInYears = experienceInYears;
        PhoneNumber = phoneNumber;
    }

    public void UpdateSocial(List<SocialNetwork> socialNetworks)
    {
        if (SocialNetworkDetails is null)
            SocialNetworkDetails = new SocialNetworkDetails();

        SocialNetworkDetails.SocialNetworks.Clear();
        SocialNetworkDetails.SocialNetworks?.AddRange(socialNetworks);
    }

    public void UpdateRequisites(List<VolunteerRequisite> requisites)
    {
        if (RequisiteDetails is null)
            RequisiteDetails = new RequisiteDetails();

        RequisiteDetails.Requisites.Clear();
        RequisiteDetails.Requisites?.AddRange(requisites);
    }

    public void AddSocialNetwork(SocialNetwork socialNetwork)
    {
        if (SocialNetworkDetails is null)
            SocialNetworkDetails = new SocialNetworkDetails();

        SocialNetworkDetails.SocialNetworks.Add(socialNetwork);
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