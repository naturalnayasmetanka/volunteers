using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;

public record AddPetPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    List<FileDTO> Photo) : ICommand;