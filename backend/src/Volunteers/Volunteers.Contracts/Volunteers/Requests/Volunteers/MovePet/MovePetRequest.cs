using Volunteers.Application.Volunteers.Commands.MovePet.Commands;

namespace Volunteers.Contracts.Volunteers.Requests.Volunteers.MovePet;

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
