namespace Volunteers.Application.DTO;

public class VolunteerDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public double ExperienceInYears { get; set; }
    public int PhoneNumber { get; set; }
    public SocialNetworkDetailsDTO? SocialNetworkDetails { get; set; }
    public RequisiteDetailsDTO? RequisiteDetails { get; set; }
}

public class SocialNetworkDetailsDTO
{
    public List<SocialNetworkDTO>? SocialNetworks { get; set; }
}

public class SocialNetworkDTO
{
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}

public class RequisiteDetailsDTO
{
    public List<RequisiteDTO>? Requisites { get; set; }
}

public class RequisiteDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
