using Serilog;
using Volunteers.API.Extentions.Middlewares;
using Volunteers.API.Extentions.WebApp;

namespace Volunteers.API.Extentions.DI;

public static class BuilderInjector
{
    public static WebApplication? AddWebApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.ApplyMigrations();
        }
        else
        {
            app.UseExceptionMiddleware();
        }


        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();


        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();

        return app;
    }
}