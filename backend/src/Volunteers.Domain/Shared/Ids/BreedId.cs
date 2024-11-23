namespace Volunteers.Domain.Shared.Ids;

public record BreedId
{
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; set; }

    public static BreedId NewVolunteerId() => new BreedId(Guid.NewGuid());
    public static BreedId EmptyVolunteerId() => new BreedId(Guid.Empty);
    public static BreedId Create(Guid id) => new BreedId(id);
}