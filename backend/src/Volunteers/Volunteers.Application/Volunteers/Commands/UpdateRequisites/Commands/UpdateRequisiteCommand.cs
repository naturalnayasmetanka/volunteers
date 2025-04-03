using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites.DTO;

namespace Volunteers.Application.Volunteers.Commands.UpdateRequisites.Commands;

public record UpdateRequisiteCommand(Guid Id, UpdateRequisiteListDTO RequisitesDTO) : ICommand;