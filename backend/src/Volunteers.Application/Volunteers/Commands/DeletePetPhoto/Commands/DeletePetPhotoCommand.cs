using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;

namespace Volunteers.Application.Volunteers.Commands.DeletePetPhoto.Commands;

public record DeletePetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    FileDTO FileData) : ICommand;