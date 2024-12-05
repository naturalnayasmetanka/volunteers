using CSharpFunctionalExtensions;
using Volunteers.Domain.PetManagment.Pet.Enums;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared;
using Volunteers.Domain.Shared.CustomErrors;
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

        if (_pets is not null)
        {
            foreach (var pet in _pets)
            {
                pet.SoftDelete();
            }
        }
    }

    public void Restore()
    {
        _isDeleted = false;

        if (_pets is not null)
        {
            foreach (var pet in _pets)
            {
                pet.Restore();
            }
        }
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
        if (RequisiteDetails is null)
            RequisiteDetails = new RequisiteDetails();

        RequisiteDetails.Requisites.Add(requisite);
    }

    public Result<PetModel, Error> AddPet(PetModel pet)
    {
        var position = Position.Create(_pets.Count + 1);

        if (position.IsFailure)
            return position.Error;

        pet.SetSerialNumber(position: position.Value);

        _pets.Add(pet);

        return pet;
    }

    public Result<List<PetModel>, Error> AddPets(List<PetModel> pets)
    {
        foreach (var pet in pets)
        {
            var position = Position.Create(_pets.Count + 1);

            if (position.IsFailure)
                return position.Error;

            pet.SetSerialNumber(position: position.Value);

            _pets.Add(pet);
        }

        return pets;
    }

    public int NumberOfAttachedAnimals()
       => Pets.Where(x => x.HelpStatus == PetStatus.FoundHome).Count();

    public int NumberOfLookingHomeAnimals()
        => Pets.Where(x => x.HelpStatus == PetStatus.LookingHome).Count();

    public int NumberOfNeedsHelpAnimals()
        => Pets.Where(x => x.HelpStatus == PetStatus.NeedsHelp).Count();

    public Result<Position, Error> MovePetPosition(PetModel pet, Position newPosition)
    {
        var currentPosition = pet.Position;

        if (currentPosition == newPosition)
            return currentPosition;

        if (_pets.Count == 1)
            return Errors.General.ValueIsInvalid("Single pet");

        var adjustedPosition = AdjustNewPositionIfOutOfRandge(newPosition);

        if (adjustedPosition.IsFailure)
            return adjustedPosition.Error;

        newPosition = adjustedPosition.Value;

        var moveResult = MovePet(newPosition, currentPosition);

        if (moveResult.IsFailure)
            return Errors.General.ValueIsInvalid("Move result");

        pet.Move(newPosition);

        return moveResult;
    }

    private Result<Position, Error> MovePet(Position newPosition, Position currentPosition)
    {
        if (newPosition.Value < currentPosition.Value)
        {
            var petsToMove = _pets
                .Where(x => x.Position.Value >= newPosition.Value
                    && x.Position.Value < currentPosition.Value);

            foreach (var pet in petsToMove)
            {
                var result = pet.MoveForward();

                if (result.IsFailure)
                    return Errors.General.ValueIsInvalid("Moving forvard");
            }
        }
        else if (newPosition.Value > currentPosition.Value)
        {
            var petsToMove = _pets
               .Where(x => x.Position.Value > currentPosition.Value
                   && x.Position.Value <= newPosition.Value);

            foreach (var pet in petsToMove)
            {
                var result = pet.MoveBack();

                if (result.IsFailure)
                {
                    return Errors.General.ValueIsInvalid("Moving forvard");
                }
            }
        }

        return newPosition;
    }

    private Result<Position, Error> AdjustNewPositionIfOutOfRandge(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
            return newPosition;

        var lastPosition = Position.Create(_pets.Count - 1);

        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
    }
}