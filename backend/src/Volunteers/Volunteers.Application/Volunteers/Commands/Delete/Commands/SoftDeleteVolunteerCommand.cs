using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.Delete.Commands;

public record SoftDeleteVolunteerCommand(Guid Id) : ICommand;
