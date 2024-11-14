namespace Volunteers.Domain.Volunteer.Models;

public record SocialNetworkDetails
{
    public List<SocialNetwork> SocialNetworks { get; private set; } = [];
}

public record VolunteerRequisiteDetails
{
    public List<VolunteerRequisite> Requisites { get; private set; } = [];
}