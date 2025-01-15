using Volunteers.Application.Volunteers.UpdateSotialNetworks.Commands;
using Volunteers.Application.Volunteers.UpdateSotialNetworks.DTO;

namespace Volunteers.API.Contracts.Volunteers.UpdateSocialNetworks;

public record UpdateSocialListRequest
{
    public List<UpdateSocialRequest> Socials { get; set; } = new List<UpdateSocialRequest>();

    public static UpdateSocialNetworksCommand ToCommand(
        Guid volunteerId, 
        UpdateSocialListRequest request)
    {
        var socials = new List<UpdateSocialDto>();
        request.Socials.ForEach(x => socials.Add(new UpdateSocialDto(x.Title, x.Link)));

        var command = new UpdateSocialNetworksCommand(
            volunteerId, 
            new UpdateSocialListDto(socials));

        return command;
    }
};