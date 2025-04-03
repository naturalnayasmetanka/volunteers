using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.SetMainPetPhoto.Commands;

public record SetMainPetPhotoCommand(Guid VolunteerId, Guid PetId, string FilePath) : ICommand;
