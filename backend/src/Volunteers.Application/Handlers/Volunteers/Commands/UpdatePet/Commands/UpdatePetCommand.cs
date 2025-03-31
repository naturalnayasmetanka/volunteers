using Volunteers.Application.Abstractions;
using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdatePet.Commands;

public record UpdatePetCommand(
    Guid PetId,
    Guid VolunteerId,
    string Nickname,
    string CommonDescription,
    string HelthDescription,
    int PetPhoneNumber,
    PetStatus PetStatus,
    DateTime BirthDate,
    DateTime CreationDate,
    Guid SpeciesId,
    Guid BreedId) : ICommand;