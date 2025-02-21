using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Providers;
using Volunteers.Application.Volunteer;
using Volunteers.Application.Volunteers.Commands.AddPet.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks;
using Volunteers.Domain.PetManagment.Pet.Entities;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Volunteers.Commands.AddPet;

public class AddPetVolunteerHandler
{
    private readonly ILogger<AddPetVolunteerHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<AddPetCommand> _validator;
    public AddPetVolunteerHandler(
        ILogger<AddPetVolunteerHandler> logger,
        IVolunteerRepository volunteerRepository,
        IMinIoProvider minIoProvider,
        IValidator<AddPetCommand> validator)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, Error>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Validation is failed into {0}", nameof(UpdateSotialNetworksHandler));

            return Error.Validation(validationResult.Errors.First().ToString(), "add.pet.volunteer");
        }

        var volunteer = await _volunteerRepository.GetByIdAsync(VolunteerId.Create(command.VolunteerId), cancellationToken);

        if (volunteer is null)
        {
            _logger.LogInformation("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(AddPetVolunteerHandler));

            return Errors.General.NotFound(command.VolunteerId);
        }

        var petId = PetId.NewPetId();

        var pet = Pet.Create(
            id: petId,
            nickname: Nickname.Create(command.Nickname).Value,
            commonDescription: CommonDescription.Create(command.CommonDescription).Value,
            helthDescription: HelthDescription.Create(command.HelthDescription).Value,
            phoneNumber: PetPhoneNumber.Create(command.PetPhoneNumber).Value,
            helpStatus: command.PetStatus,
            birthDate: command.BirthDate,
            creationDate: command.CreationDate,
            volunteerId: VolunteerId.Create(command.VolunteerId)).Value;

        var addResult = volunteer.AddPet(pet);

        if (addResult.IsFailure)
        {
            _logger.LogError("Pet {0} was not added to volunteer {1} into {2}", petId, command.VolunteerId, nameof(AddPetVolunteerHandler));

            return addResult.Error;
        }

        await _volunteerRepository.SaveAsync();

        _logger.LogInformation("Pet {0} was added to volunteer {1} into {2}", petId, command.VolunteerId, nameof(AddPetVolunteerHandler));

        return (Guid)pet.Id;
    }
}