using Shared.Core.Extentions.DI;

namespace Volunteers.API.Extentions.DI;

public static class SharedInjector
{
    public static IServiceCollection AddSharedDI(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSharedCore(builder);

        return services;
    }
}
