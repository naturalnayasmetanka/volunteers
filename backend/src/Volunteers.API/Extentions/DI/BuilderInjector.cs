namespace Volunteers.API.Extentions.DI;

public static class BuilderInjector
{
    public static WebApplication? AddWebApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();

        return app;
    }
}