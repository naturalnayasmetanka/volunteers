namespace Volunteers.Domain.Volunteers.ValueObjects;

public class RequisiteDetails
{
    public List<VolunteerRequisite> Requisites { get; private set; } = [];
}

public class SocialNetworkDetails
{
    public List<SocialNetwork> SocialNetworks { get; private set; } = [];
}