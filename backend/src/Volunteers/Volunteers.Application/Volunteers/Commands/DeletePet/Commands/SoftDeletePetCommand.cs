using Shared.Core.Abstractions.Handlers;

namespace Volunteers.Application.Volunteers.Commands.DeletePet.Commands;

public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
