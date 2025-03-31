using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites.DTO;

namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites.Commands;

public record UpdateRequisiteCommand(Guid Id, UpdateRequisiteListDTO RequisitesDTO) : ICommand;