using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto.Commands;

public record GetPresignedLinkPhotoCommand(FileDTO FileData) : ICommand;
