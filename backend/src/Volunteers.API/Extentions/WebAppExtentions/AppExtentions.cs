using Microsoft.EntityFrameworkCore;
using Volunteers.Infrastructure.Contexts;

namespace Volunteers.API.Extentions.WebAppExtentions;

public static class AppExtentions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}