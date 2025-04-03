using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.MovePet.Commands;

public record MovePetCommand(
    Guid VolunteerId,
    Guid PetId,
    int NewPosition) : ICommand;
