using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;

namespace Volunteers.Application.Handlers.Volunteers.Commands.AddPetPhoto.Commands;

public record AddPetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    List<FileDTO> Photo) : ICommand;