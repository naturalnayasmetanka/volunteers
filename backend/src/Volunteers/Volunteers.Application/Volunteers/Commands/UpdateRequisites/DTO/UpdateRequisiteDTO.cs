namespace Volunteers.Application.Volunteers.Commands.UpdateRequisites.DTO;

public record UpdateRequisiteDTO(string Title, string Description);

public record UpdateRequisiteListDTO(List<UpdateRequisiteDTO> RequisiteList);