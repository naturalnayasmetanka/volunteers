using Shared.Core.Extentions.di;

namespace Volunteers.API.Extentions.DI;

public static class SharedInjector
{
    public static IServiceCollection AddSharedDI(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSharedCore(builder);

        return services;
    }
}
