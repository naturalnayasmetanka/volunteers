namespace Shared.Kernel;

public interface ISoftDeletable
{
    void SoftDelete();
    void Restore();
}
