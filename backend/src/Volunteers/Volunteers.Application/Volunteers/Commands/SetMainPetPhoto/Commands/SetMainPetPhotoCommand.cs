using Shared.Core.Abstractions.Handlers;

namespace Volunteers.Application.Volunteers.Commands.SetMainPetPhoto.Commands;

public record SetMainPetPhotoCommand(Guid VolunteerId, Guid PetId, string FilePath) : ICommand;
