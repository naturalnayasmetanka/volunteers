namespace Volunteers.Application.Volunteers.UpdateSotialNetworks.DTO;
public record UpdateSocialDto(string Title, string Link);

public record UpdateSocialListDto(List<UpdateSocialDto> ListSocial);
