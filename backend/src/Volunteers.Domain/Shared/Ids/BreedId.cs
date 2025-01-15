namespace Volunteers.Domain.Shared.Ids;

public record BreedId
{
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; set; }

    public static BreedId NewBreedId() => new BreedId(Guid.NewGuid());
    public static BreedId EmptyBreedId() => new BreedId(Guid.Empty);
    public static BreedId Create(Guid id) => new BreedId(id);
}