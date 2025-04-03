using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.UpdatePetStatus.Commands;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, PetStatus PetStatus) : ICommand;
