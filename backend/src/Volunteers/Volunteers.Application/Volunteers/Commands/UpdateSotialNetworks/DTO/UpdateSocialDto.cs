using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks.DTO;

public record UpdateSocialDto(string Title, string Link);

public record UpdateSocialListDto(List<UpdateSocialDto> ListSocial);