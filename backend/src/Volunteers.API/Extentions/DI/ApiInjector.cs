using System.Text.Json.Serialization;

namespace Volunteers.API.Extentions.DI;

public static class ApiInjector
{
    public static IServiceCollection AddApi(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x =>
        {
            x.EnableAnnotations();
        });

        return services;
    }
}