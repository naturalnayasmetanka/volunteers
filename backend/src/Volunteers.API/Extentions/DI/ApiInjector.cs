using System.Text.Json.Serialization;

namespace Volunteers.API.Extentions.DI;

public static class ApiInjector
{
    public static IServiceCollection AddApi(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}