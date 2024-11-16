namespace Volunteers.Domain.Shared.Models;

public abstract class Entity<TId> where TId : notnull
{
    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; private set; }
}