using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.Enums;

namespace Volunteers.Application.Volunteers.Commands.UpdatePetStatus.Commands;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, PetStatus PetStatus) : ICommand;
