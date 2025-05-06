namespace Shared.Kernel.Ids;

public record SpeciesId
{
    private SpeciesId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; set; }

    public static SpeciesId NewSpeciesId() => new SpeciesId(Guid.NewGuid());
    public static SpeciesId EmptySpeciesId() => new SpeciesId(Guid.Empty);
    public static SpeciesId Create(Guid id) => new SpeciesId(id);
}
