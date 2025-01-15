using Volunteers.Application.Volunteers.UpdateSotialNetworks.DTO;

namespace Volunteers.Application.Volunteers.UpdateSotialNetworks.Commands;

public record UpdateSocialNetworksCommand(
    Guid Id,
    UpdateSocialListDto SocialListDto);
