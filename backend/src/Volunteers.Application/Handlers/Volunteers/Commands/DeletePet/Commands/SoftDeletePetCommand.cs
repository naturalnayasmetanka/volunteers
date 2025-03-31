using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Volunteers.Commands.DeletePet.Commands;

public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
