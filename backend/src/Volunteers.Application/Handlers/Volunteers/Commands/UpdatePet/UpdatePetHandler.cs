using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePet.Commands;
using Volunteers.Domain.PetManagment.Pet.Entities;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdatePet;

public class UpdatePetHandler : ICommandHandler<Guid, UpdatePetCommand>
{
    private readonly ILogger<UpdatePetHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdatePetCommand> _validator;

    public UpdatePetHandler(
        ILogger<UpdatePetHandler> logger,
        IVolunteerRepository volunteerRepository,
        IValidator<UpdatePetCommand> validator)
    {
        _logger = logger;
        _volunteerRepository = volunteerRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdatePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Validation is failed into {0}", nameof(UpdatePetHandler));

            return Error.Validation(validationResult.Errors.First().ToString(), "update.pet.volunteer");
        }

        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteer = await _volunteerRepository.GetByIdAsync(volunteerId, cancellationToken);

        if (volunteer is null)
        {
            _logger.LogInformation("Volunteer {0} was not found into {1}", command.VolunteerId, nameof(UpdatePetHandler));

            return Errors.General.NotFound(command.VolunteerId);
        }

        var pet = Pet.Create(
            id: PetId.Create(command.PetId),
            nickname: Nickname.Create(command.Nickname).Value,
            commonDescription: CommonDescription.Create(command.CommonDescription).Value,
            helthDescription: HelthDescription.Create(command.HelthDescription).Value,
            phoneNumber: PetPhoneNumber.Create(command.PetPhoneNumber).Value,
            helpStatus: command.PetStatus,
            birthDate: command.BirthDate,
            creationDate: command.CreationDate,
            volunteerId: VolunteerId.Create(command.VolunteerId),
            speciesBreed: SpeciesBreed.Create(speciesId: command.SpeciesId, breedId: command.BreedId).Value
            ).Value;

        var updateResult = volunteer.UpdatePet(pet, command.PetId);

        if (updateResult.IsFailure)
        {
            _logger.LogInformation("Pet {0} was not updated {1}", command.PetId, nameof(UpdatePetHandler));

            return Error.Failure(updateResult.Error.Message, "update.pet.volunteer");
        }

        await _volunteerRepository.UpdateAsync(volunteer, cancellationToken);

        _logger.LogInformation("Pet {0} was updated into {1}", command, nameof(UpdatePetHandler));

        return (Guid)updateResult.Value.Id;
    }
}
