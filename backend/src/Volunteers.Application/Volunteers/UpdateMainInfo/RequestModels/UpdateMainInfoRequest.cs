using Volunteers.Application.Volunteers.UpdateMainInfo.DTO;

namespace Volunteers.Application.Volunteers.UpdateMainInfo.RequestModels;

public record UpdateMainInfoRequest(Guid Id, UpdateMainInfoDto MainInfoDto);