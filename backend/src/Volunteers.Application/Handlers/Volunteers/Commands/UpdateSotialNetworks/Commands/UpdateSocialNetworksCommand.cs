using Volunteers.Application.Abstractions;
using Volunteers.Application.Handlers.Volunteers.Commands.UpdateSotialNetworks.DTO;

namespace Volunteers.Application.Handlers.Volunteers.Commands.UpdateSotialNetworks.Commands;

public record UpdateSocialNetworksCommand(
    Guid Id,
    UpdateSocialListDto SocialListDto) : ICommand;
