using Microsoft.Extensions.DependencyInjection;
using Species.Contracts.Species;
using Species.Presentation.ContractsEmplementations;

namespace Species.Presentation.Extentions.di;

public static class SpeciesPresentationInjector
{
    public static IServiceCollection AddSpeciesPresentation(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesContract, SpeciesContract>();

        return services;
    }
}
