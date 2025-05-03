using Shared.Kernel.Enums;
using Volunteers.Application.Volunteers.Commands.UpdatePetStatus.Commands;

namespace Volunteers.Contracts.Volunteers.Requests.Volunteers.UpdatePetStatus;

public record UpdatePetStatusRequest
{
    public PetStatus Status { get; set; }

    public static UpdatePetStatusCommand ToCommand(Guid volunteerId, Guid petId, UpdatePetStatusRequest request)
    {
        var command = new UpdatePetStatusCommand(VolunteerId: volunteerId, PetId: petId, request.Status);

        return command;
    }
}
