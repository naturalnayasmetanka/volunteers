﻿
namespace Volunteers.API.Extentions.WebApp;

public static class AppExtentions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        //var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}