using Volunteers.Application.Volunteers.UpdateRequisites.DTO;

namespace Volunteers.Application.Volunteers.UpdateRequisites.RequestModels;

public record UpdateRequisiteRequest(Guid Id, UpdateRequisiteListDTO RequisitesDTO);