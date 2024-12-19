using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Providers;
using Volunteers.Application.Providers.Models;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.AddPet.Commands;
using Volunteers.Domain.PetManagment.Pet.Entities;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;
using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;
namespace Volunteers.Application.Volunteers.AddPet;

public class AddPetVolunteerHandler
{
    private const string BUCKET_NAME = "photos";

    private List<Error> _errors = [];

    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IMinIoProvider _minIoProvider;
    public AddPetVolunteerHandler(
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository,
        IMinIoProvider minIoProvider)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _minIoProvider = minIoProvider;
    }

    public async Task<Result<VolunteerModel, List<Error>>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _volunteerRepository
            .GetByIdAsync(VolunteerId.Create(command.VolunteerId), cancellationToken);

        if (volunteer is null)
        {
            _errors.Add(Errors.General.NotFound(command.VolunteerId));
            _logger.LogInformation("Volunteer was not found with id: {0}", command.VolunteerId);

            return _errors;
        }

        var petId = PetId.NewPetId();
        var volunteerId = VolunteerId.Create(command.VolunteerId);

        var nickname = Nickname.Create(command.Nickname).Value;
        var commonDescription = CommonDescription.Create(command.CommonDescription).Value;
        var helthDescription = HelthDescription.Create(command.HelthDescription).Value;
        var phoneNumber = PetPhoneNumber.Create(command.PetPhoneNumber).Value;
        var helpStatus = command.PetStatus;
        var birthDate = command.BirthDate;
        var creationDate = command.CreationDate;

        List<PetPhoto> urls = [];

        foreach (var file in command.Files)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fileData = new FileData(file.FileStream, BUCKET_NAME, fileName);

            var urlResult = await _minIoProvider.UploadAsync(fileData, cancellationToken);

            urls.Add(PetPhoto.Create(urlResult.Value).Value);
        }

        var pet = Pet.Create(
            id: petId,
            nickname: nickname,
            commonDescription: commonDescription,
            helthDescription: helthDescription,
            phoneNumber: phoneNumber,
            helpStatus: helpStatus,
            birthDate: birthDate,
            creationDate: creationDate,
            volunteerId: volunteerId).Value;

        urls.ForEach(url => pet.AddPhoto(url));

        var result = volunteer.AddPet(pet);

        if (result.IsFailure)
        {
            _errors.Add(result.Error);
            _logger.LogInformation("Pet with id {0} was not added to volunteer with id: {1}", petId, command.VolunteerId);

            return _errors;
        }

        await _volunteerRepository.SaveAsync();

        _logger.LogInformation("Pet with id {0} was added to volunteer with id: {1}", petId, command.VolunteerId);

        return volunteer;
    }
}