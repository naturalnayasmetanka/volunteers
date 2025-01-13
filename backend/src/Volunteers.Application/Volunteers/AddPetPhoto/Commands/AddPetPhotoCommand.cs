using Volunteers.Application.DTO;

namespace Volunteers.Application.Volunteers.AddPetPhoto.Commands;

public record AddPetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    List<FileDTO> Photo);