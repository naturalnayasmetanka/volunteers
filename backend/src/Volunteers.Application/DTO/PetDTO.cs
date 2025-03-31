using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.Application.DTO;

public class PetDTO
{
    public Guid id { get; set; }
    public string nickname { get; set; } = string.Empty;
    public string common_description { get; set; } = string.Empty;
    public string helth_description { get; set; } = string.Empty;
    public int phone_number { get; set; }
    public PetStatus help_status { get; set; }
    public DateTime birth_date { get; set; }
    public DateTime creation_date { get; set; }
    public int position { get; set; }

    public LocationDetailsDTO? LocationDetails { get; set; }
    public RequisitesDetailsDTO? RequisitesDetails { get; set; }
    public PhotoDetailsDTO? PhotoDetails { get; set; }
    public PhysicalParametersDetailsDTO? PhysicalParametersDetails { get; set; }

    public Guid volunteer_id { get; set; }

    public SpeciesBreedDTO? SpeciesBreed { get; set; }


}

public class LocationDetailsDTO
{
    public List<LocationDTO> Location { get; set; } = [];
}

public class LocationDTO
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public int Floor { get; set; }
}

public class RequisitesDetailsDTO
{
    public List<RequisitesDTO> PetRequisites { get; set; } = [];
}

public class RequisitesDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class PhotoDetailsDTO
{
    public List<PhotoDTO>? PhotoDetails { get; set; }
}

public class PhotoDTO
{
    public string Path { get; set; } = string.Empty;
    public bool IsMain { get; set; }
}

public class PhysicalParametersDetailsDTO
{
    public List<PhysicalParametersDTO> PhysicalParameters { get; set; } = [];
}

public class PhysicalParametersDTO
{
    public string Type { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string? Breed { get; set; }
    public string? Color { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public bool IsSterilized { get; set; }
    public bool IsVaccinated { get; set; }
}

public class SpeciesBreedDTO
{
    public Guid SpeciesId { get; set; }
    public Guid BreedId { get; set; }
}