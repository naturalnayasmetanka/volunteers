using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;

namespace Volunteers.Application.Volunteers.Commands.DeletePetPhoto.Commands;

public record DeletePetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    FileDTO FileData) : ICommand;
