using Volunteers.Application.Volunteers.UpdateRequisites.DTO;

namespace Volunteers.Application.Volunteers.UpdateRequisites.Commands;

public record UpdateRequisiteCommand(Guid Id, UpdateRequisiteListDTO RequisitesDTO);