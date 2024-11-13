namespace Volunteers.Domain.Pet.Models
{
    public record PetDetailsLocation
    {
        public List<Location> Locations { get; private set; } = [];
    }

    public record PetDetailsRequisite
    {
        public List<PetRequisite> Requisites { get; private set; } = [];
    }

    public record PetDetailsPhoto
    {
        public List<PetPhoto> Photo { get; private set; } = [];
    }

    public record PetDetailsPhysicalParameters
    {
        public List<PhysicalParameters> PhysicalParameters { get; private set; } = [];
    }
}