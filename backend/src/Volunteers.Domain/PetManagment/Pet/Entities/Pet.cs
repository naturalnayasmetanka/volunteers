﻿using CSharpFunctionalExtensions;
using Volunteers.Domain.PetManagment.Pet.Enums;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;
using CustomEntity = Volunteers.Domain.Shared;
using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;

namespace Volunteers.Domain.PetManagment.Pet.Entities;

public class Pet : CustomEntity.Entity<PetId>, ISoftDeletable
{
    private bool _IsDelete = false;

    private Pet(PetId id) : base(id) { }

    private Pet(
        PetId id,
        Nickname nickname,
        CommonDescription commonDescription,
        HelthDescription helthDescription,
        PetPhoneNumber phoneNumber,
        PetStatus helpStatus,
        DateTime birthDate,
        DateTime creationDate,
        VolunteerId volunteerId
        ) : base(id)
    {
        Nickname = nickname;
        CommonDescription = commonDescription;
        HelthDescription = helthDescription;
        PhoneNumber = phoneNumber;
        HelpStatus = helpStatus;
        BirthDate = birthDate;
        CreationDate = creationDate;
        VolunteerId = volunteerId;
    }

    public Nickname Nickname { get; private set; } = default!;
    public CommonDescription CommonDescription { get; private set; } = default!;
    public HelthDescription HelthDescription { get; private set; } = default!;
    public PetPhoneNumber PhoneNumber { get; private set; } = default!;
    public PetStatus HelpStatus { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public DateTime CreationDate { get; private set; }
    public Position Position { get; private set; } = default!;

    public VolunteerId VolunteerId { get; private set; } = default!;
    public VolunteerModel Volunteer { get; private set; } = default!;

    public LocationDetails? LocationDetails { get; private set; }
    public RequisitesDetails? RequisitesDetails { get; private set; }
    public PhotoDetails? PhotoDetails { get; private set; }
    public PhysicalParametersDetails? PhysicalParametersDetails { get; private set; }

    public SpeciesBreed? SpeciesBreed { get; private set; } = default!;

    public static Result<Pet> Create(
        PetId id,
        Nickname nickname,
        CommonDescription commonDescription,
        HelthDescription helthDescription,
        PetPhoneNumber phoneNumber,
        PetStatus helpStatus,
        DateTime birthDate,
        DateTime creationDate,
        VolunteerId volunteerId)
    {
        var newPet = new Pet(
            id: id,
            nickname: nickname,
            commonDescription: commonDescription,
            helthDescription: helthDescription,
            phoneNumber: phoneNumber,
            helpStatus: helpStatus,
            birthDate: birthDate,
            creationDate: creationDate,
            volunteerId: volunteerId);

        return Result.Success(newPet);
    }

    public void SetSerialNumber(Position position)
    {
        Position = position;
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
        if (LocationDetails is null)
            LocationDetails = new LocationDetails();

        LocationDetails.Locations.Add(location);
    }

    public void AddRequisite(PetRequisite requisite)
    {
        if (RequisitesDetails is null)
            RequisitesDetails = new RequisitesDetails();

        RequisitesDetails.PetRequisites.Add(requisite);
    }

    public void AddPhysicalParameters(PhysicalParameters physicalParameters)
    {
        if (PhysicalParametersDetails is null)
            PhysicalParametersDetails = new PhysicalParametersDetails();

        PhysicalParametersDetails.PhysicalParameters.Add(physicalParameters);
    }

    public void AddPhoto(PetPhoto photo)
    {
        if (PhotoDetails is null)
            PhotoDetails = new PhotoDetails();

        PhotoDetails.PetPhoto.Add(photo);
    }

    public void UpdatePhoto(List<PetPhoto> photo)
    {
        if (PhotoDetails is null)
            PhotoDetails = new PhotoDetails();

        PhotoDetails.PetPhoto.Clear();

        if (photo.Any())
        {
            PhotoDetails.PetPhoto.AddRange(photo);
        }
        else
        {
            PhotoDetails = null;
        }
    }

    public Result<Position, Error> MoveForward()
    {
        var newPosition = Position.Forward();

        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Position;
    }

    public Result<Position, Error> MoveBack()
    {
        var newPosition = Position.Back();

        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Position;
    }

    public void Move(Position position)
    {
        Position = position;
    }
}