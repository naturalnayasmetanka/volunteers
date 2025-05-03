using Volunteers.Application.Volunteers.Commands.SetMainPetPhoto.Commands;

namespace Volunteers.Contracts.Volunteers.Requests.Volunteers.SetMainPetPhoto;

public record SetMainPetPhotoRequest
{
    public string FilePath { get; set; } = string.Empty;

    public static SetMainPetPhotoCommand ToCommand(Guid volunteerId, Guid petId, SetMainPetPhotoRequest request)
    {
        return new SetMainPetPhotoCommand(VolunteerId: volunteerId, PetId: petId, FilePath: request.FilePath);
    }
}
