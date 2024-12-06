using Serilog;

namespace Volunteers.API.Extentions.DI;

public static class ApiInjector
{
    public static IServiceCollection AddApi(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddLogger(builder);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSerilog();

        return services;
    }

    private static IServiceCollection AddLogger(this IServiceCollection services, WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")
                            ?? throw new ApplicationException("Seq connection not found in appsettings"))
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Roating", Serilog.Events.LogEventLevel.Warning)
            .CreateLogger();

        return services;
    }
}