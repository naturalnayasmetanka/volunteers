namespace Volunteers.Application.Volunteers.MovePet.Commands;

public record MovePetCommand(
    Guid VolunteerId,
    Guid PetId,
    int NewPosition);
