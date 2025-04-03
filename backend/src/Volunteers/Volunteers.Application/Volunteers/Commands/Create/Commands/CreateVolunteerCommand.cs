using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteers.Application.Volunteers.Commands.Create.DTO;

namespace Volunteers.Application.Volunteers.Commands.Create.Commands;

public record CreateVolunteerCommand(CreateVolunteerDto VolunteerDto) : ICommand;
