using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.UpdateRequisites.DTO;

public record UpdateRequisiteDTO(string Title, string Description);

public record UpdateRequisiteListDTO(List<UpdateRequisiteDTO> RequisiteList);