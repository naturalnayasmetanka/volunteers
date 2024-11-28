namespace Volunteers.Domain.Shared;

public interface ISoftDeletable
{
    void SoftDelete();
    void Restore();
}