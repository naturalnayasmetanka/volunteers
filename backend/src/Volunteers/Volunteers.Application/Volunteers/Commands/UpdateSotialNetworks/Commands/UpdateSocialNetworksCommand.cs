using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks.DTO;

namespace Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks.Commands;

public record UpdateSocialNetworksCommand(
    Guid Id,
    UpdateSocialListDto SocialListDto) : ICommand;