using Species.Application.Extentions.DI;
using Species.Infrastructure.Extentions.DI;

namespace Volunteers.API.Extentions.DI;

public static class SpeciesInjector
{
    public static IServiceCollection AddSpeciesDI(this IServiceCollection services)
    {
        services.AddSpeciesInfrastructure();
        services.AddSpeciesBreedApplication();

        return services;
    }
}
