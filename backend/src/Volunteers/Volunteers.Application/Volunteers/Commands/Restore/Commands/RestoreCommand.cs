using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.Restore.Commands;

public record RestoreCommand(Guid Id) : ICommand;
