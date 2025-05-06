using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;

namespace Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;

public record AddPetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    List<FileDTO> Photo) : ICommand;