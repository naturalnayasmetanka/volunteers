namespace Volunteers.Application.Volunteers.UpdateRequisites.DTO;

public record UpdateRequisiteDTO(string Title, string Description);

public record UpdateRequisiteListDTO(List<UpdateRequisiteDTO> RequisiteList);