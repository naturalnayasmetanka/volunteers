using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Volunteers.Commands.MovePet.Commands;

public record MovePetCommand(
    Guid VolunteerId,
    Guid PetId,
    int NewPosition) : ICommand;
