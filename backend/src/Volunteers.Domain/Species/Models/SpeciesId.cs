namespace Volunteers.Domain.Species.Models;

public record SpeciesId
{
    private SpeciesId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; set; }

    public static SpeciesId NewVolunteerId() => new SpeciesId(Guid.NewGuid());
    public static SpeciesId EmptyVolunteerId() => new SpeciesId(Guid.Empty);
    public static SpeciesId Create(Guid id) => new SpeciesId(id);
}