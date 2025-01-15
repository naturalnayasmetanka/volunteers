using Volunteers.Application.DTO;

namespace Volunteers.Application.Volunteers.DeletePetPhoto.Commands;

public record DeletePetPhotoCommand(
    Guid VolunteerId, 
    Guid PetId,
    FileDTO FileData);