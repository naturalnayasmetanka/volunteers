using Volunteers.Application.Handlers.Volunteers.Commands.MovePet.Commands;

namespace Volunteers.API.Contracts.Volunteers.MovePet;

public record MovePetRequest
{
    public Guid PetId { get; set; }
    public int NewPosition { get; set; }

    public static MovePetCommand ToCommand(Guid volunteerId, MovePetRequest request)
    {
        var command = new MovePetCommand(
            VolunteerId: volunteerId,
            PetId: request.PetId,
            NewPosition: request.NewPosition);

        return command;
    }
}
