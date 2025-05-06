using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions.Database;
using Shared.Core.Enums;
using Species.Infrastructure.Contexts;

namespace Species.Infrastructure.Extentions.DI;

public static class SpeciesInfrastructureInjector
{
    public static IServiceCollection AddSpeciesInfrastructure(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, SpeciesUnitOfWork>(UoWServiceDI.SpeciesService);
        services.AddScoped<SpeciesDbContext>();

        return services;
    }
}
