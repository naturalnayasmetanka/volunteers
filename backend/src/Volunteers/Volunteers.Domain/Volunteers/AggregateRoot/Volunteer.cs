using CSharpFunctionalExtensions;
using Shared.Kernel;
using Shared.Kernel.CustomErrors;
using Shared.Kernel.Enums;
using Shared.Kernel.Ids;
using Volunteers.Domain.Pets.ValueObjects;
using Volunteers.Domain.Volunteers.ValueObjects;
using CustomEntity = Shared.Kernel;

using PetModel = Volunteers.Domain.Pets.Entities.Pet;

namespace Volunteers.Domain.Volunteers.AggregateRoot;

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


    #region Volunteer

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

    #endregion

    #region Pet

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

    public Result<Guid, Error> UpdatePet(
        PetId petId,
        Nickname nickname,
        CommonDescription commonDescription,
        HelthDescription helthDescription,
        PetPhoneNumber phoneNumber,
        PetStatus petStatus,
        DateTime birthDate,
        DateTime creationDate,
        VolunteerId volunteerId,
        SpeciesBreed speciesBreed)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId.Value);

        if (pet is null)
            return Errors.General.NotFound(petId);

        var result = pet.Update(
            id: petId,
            nickname: nickname,
            commonDescription: commonDescription,
            helthDescription: helthDescription,
            phoneNumber: phoneNumber,
            helpStatus: petStatus,
            birthDate: birthDate,
            creationDate: creationDate,
            volunteerId: volunteerId,
            speciesBreed: speciesBreed);

        return petId.Value;
    }

    public Result<PetModel, Error> UpdatePetStatus(PetStatus newStatus, PetId petId)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId.Value);

        if (pet is null)
            return Errors.General.NotFound(petId);

        pet.UpdateStatus(newStatus);

        return pet;
    }

    public Result<PetModel, Error> SetMainPetPhoto(string path, PetId petId)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId.Value);

        if (pet is null)
            return Errors.General.NotFound(petId);

        if (pet.PhotoDetails is null)
            return Errors.General.NotFound(petId);

        pet.SetMainPhoto(path);

        return pet;
    }

    public Result<PetModel, Error> SoftDeletePet(PetId petId)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId.Value);

        if (pet is null)
            return Errors.General.NotFound(petId);

        pet.SoftDelete();

        return pet;
    }

    public Result<List<PetModel>, Error> HardDeletePet(PetModel pet)
    {
        if (pet is null)
            return Errors.General.NotFound(pet?.Id.Value);

        _pets.Remove(pet);

        return _pets;
    }

    public int NumberOfAttachedAnimals()
       => Pets.Where(x => x.HelpStatus == PetStatus.FoundHome).Count();

    public int NumberOfLookingHomeAnimals()
        => Pets.Where(x => x.HelpStatus == PetStatus.LookingHome).Count();

    public int NumberOfNeedsHelpAnimals()
        => Pets.Where(x => x.HelpStatus == PetStatus.NeedsHelp).Count();

    public Result<Position, Error> MovePetPosition(PetId petId, Position newPosition)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId.Value);

        if (pet is null)
            return Errors.General.NotFound(petId);

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

    #endregion
}