using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Volunteers.Commands.SetMainPetPhoto.Commands;

public record SetMainPetPhotoCommand(Guid VolunteerId, Guid PetId, string FilePath) : ICommand;
