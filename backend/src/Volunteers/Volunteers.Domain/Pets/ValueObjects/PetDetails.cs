namespace Volunteers.Domain.Pets.ValueObjects;

public class LocationDetails
{
    public List<Location> Locations { get; private set; } = [];
}

public class RequisitesDetails
{
    public List<PetRequisite> PetRequisites { get; private set; } = [];
}

public class PhotoDetails
{
    public List<PetPhoto> PetPhoto { get; private set; } = [];
}

public class PhysicalParametersDetails
{
    public List<PhysicalParameters> PhysicalParameters { get; private set; } = [];
}