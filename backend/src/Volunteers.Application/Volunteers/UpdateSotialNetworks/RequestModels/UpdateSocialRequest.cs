using Volunteers.Application.Volunteers.UpdateSotialNetworks.DTO;

namespace Volunteers.Application.Volunteers.UpdateSotialNetworks.RequestModels;

public record UpdateSocialRequest(Guid Id, UpdateSocialListDto SocialListDto);