using Microsoft.OpenApi.Models;
using Species.Presentation.Controllers;
using Volunteers.Presentation.Controllers;

namespace Volunteers.API.Extentions.DI;

public static class ApiInjector
{
    public static IServiceCollection AddApi(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Volunteers",
                Version = "v1"
            });

            x.EnableAnnotations();
        });

        return services;
    }
}