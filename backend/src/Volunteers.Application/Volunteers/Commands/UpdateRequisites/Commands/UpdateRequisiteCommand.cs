using Volunteers.Application.Volunteers.Commands.UpdateRequisites.DTO;

namespace Volunteers.Application.Volunteers.Commands.UpdateRequisites.Commands;

public record UpdateRequisiteCommand(Guid Id, UpdateRequisiteListDTO RequisitesDTO);