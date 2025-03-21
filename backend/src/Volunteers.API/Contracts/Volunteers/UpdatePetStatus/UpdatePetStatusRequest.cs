using Volunteers.Application.Handlers.Volunteers.Commands.UpdatePetStatus.Commands;
using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.API.Contracts.Volunteers.UpdatePetStatus;

public record UpdatePetStatusRequest
{
    public PetStatus Status { get; set; }

    public static UpdatePetStatusCommand ToCommand(Guid volunteerId, Guid petId, UpdatePetStatusRequest request)
    {
        var command = new UpdatePetStatusCommand(VolunteerId: volunteerId, PetId: petId, request.Status);

        return command;
    }
}
