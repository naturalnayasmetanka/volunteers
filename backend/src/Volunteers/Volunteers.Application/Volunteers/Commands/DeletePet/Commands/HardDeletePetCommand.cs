using Shared.Core.Abstractions.Handlers;

namespace Volunteers.Application.Volunteers.Commands.DeletePet.Commands;

public record HardDeletePetCommand(Guid VolunteerId, Guid PetId, string BucketName) : ICommand;
