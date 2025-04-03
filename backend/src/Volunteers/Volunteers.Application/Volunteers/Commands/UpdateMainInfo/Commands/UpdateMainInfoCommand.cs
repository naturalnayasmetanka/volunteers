using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.DTO;

namespace Volunteers.Application.Volunteers.Commands.UpdateMainInfo.Commands;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    UpdateMainInfoDto MainInfoDto) : ICommand;
