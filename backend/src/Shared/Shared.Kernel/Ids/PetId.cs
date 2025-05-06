namespace Shared.Kernel.Ids;

public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetId NewPetId() => new PetId(Guid.NewGuid());
    public static PetId EmptyGuid() => new PetId(Guid.Empty);
    public static PetId Create(Guid id) => new PetId(id);

    public static implicit operator Guid(PetId petId)
    {
        ArgumentNullException.ThrowIfNull(petId);

        return petId.Value;
    }
}
