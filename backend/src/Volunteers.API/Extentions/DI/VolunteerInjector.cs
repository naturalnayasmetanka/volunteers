using Volunteers.Application.Extentions.DI;
using Volunteers.Infrastructure.Extentions.DI;

namespace Volunteers.API.Extentions.DI;

public static class VolunteerInjector
{
    public static IServiceCollection AddVolunteerDI(this IServiceCollection services)
    {
        services.AddVolunteerInfrastructure();
        services.AddVolunteersApplication();

        return services;
    }
}
