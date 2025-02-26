using Volunteers.Application.Abstractions;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks.DTO;

namespace Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks.Commands;

public record UpdateSocialNetworksCommand(
    Guid Id,
    UpdateSocialListDto SocialListDto) : ICommand;
