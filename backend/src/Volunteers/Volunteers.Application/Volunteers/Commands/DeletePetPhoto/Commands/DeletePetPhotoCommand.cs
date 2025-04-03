using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.DeletePetPhoto.Commands;

public record DeletePetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    FileDTO FileData) : ICommand;
