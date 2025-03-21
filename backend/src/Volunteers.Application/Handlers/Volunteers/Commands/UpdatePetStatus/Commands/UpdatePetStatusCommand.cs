using Volunteers.Application.Abstractions;
using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdatePetStatus.Commands;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, PetStatus PetStatus) : ICommand;
