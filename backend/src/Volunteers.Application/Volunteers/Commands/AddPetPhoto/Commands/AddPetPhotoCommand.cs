using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;

namespace Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;

public record AddPetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    List<FileDTO> Photo) : ICommand;