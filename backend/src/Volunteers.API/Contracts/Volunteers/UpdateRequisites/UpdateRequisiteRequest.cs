﻿using Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites.Commands;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateRequisites.DTO;

namespace Volunteers.API.Contracts.Volunteers.UpdateRequisites;

public record UpdateRequisiteRequest
{
    public List<UpdateRequisiteDTO> RequisiteList { get; set; } = new List<UpdateRequisiteDTO>();

    public static UpdateRequisiteCommand ToCommand(
        Guid volunteerId,
        UpdateRequisiteRequest request)
    {
        var requisitesDto = new UpdateRequisiteListDTO(request.RequisiteList);

        var command = new UpdateRequisiteCommand(
            volunteerId,
            requisitesDto);

        return command;
    }
}
