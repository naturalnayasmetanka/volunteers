using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.DeletePet.Commands;

public record HardDeletePetCommand(Guid VolunteerId, Guid PetId, string BucketName) : ICommand;
