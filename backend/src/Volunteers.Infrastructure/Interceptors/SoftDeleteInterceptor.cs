using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Volunteers.Infrastructure.Interceptors;

public class SoftDeleteInterceptor: SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        return await base.SavedChangesAsync(eventData, result, cancellationToken);    
    }
}