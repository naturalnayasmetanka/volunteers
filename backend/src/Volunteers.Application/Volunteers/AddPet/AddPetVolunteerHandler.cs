using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Providers;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.AddPet.Commands;
using Volunteers.Domain.PetManagment.Pet.Entities;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.AddPet;

public class AddPetVolunteerHandler
{
    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;

    public AddPetVolunteerHandler(
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository,
        IMinIoProvider minIoProvider)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
    }

    public async Task<Result<Guid, Error>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _volunteerRepository
            .GetByIdAsync(VolunteerId.Create(command.VolunteerId), cancellationToken);

        if (volunteer is null)
        {
            _logger.LogInformation("Volunteer was not found with id: {0}", command.VolunteerId);

            return Errors.General.NotFound(command.VolunteerId);
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

        var result = volunteer.AddPet(pet);

        if (result.IsFailure)
        {
            _logger.LogInformation("Pet with id {0} was not added to volunteer with id: {1}", petId, command.VolunteerId);

            return result.Error;
        }

        await _volunteerRepository.SaveAsync();

        _logger.LogInformation("Pet with id {0} was added to volunteer with id: {1}", petId, command.VolunteerId);

        return (Guid)pet.Id;
    }
}