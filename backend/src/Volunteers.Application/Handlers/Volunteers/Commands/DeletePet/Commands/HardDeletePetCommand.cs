
using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Handlers.Volunteers.Commands.DeletePet.Commands;

public record HardDeletePetCommand(Guid VolunteerId, Guid PetId, string BucketName) : ICommand;