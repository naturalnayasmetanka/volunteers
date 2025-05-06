using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Core.Models;
using Species.Application.Breeds.Handlers.Queries.GetBreed;
using Species.Application.Breeds.Handlers.Queries.GetBreed.Queries;
using Species.Application.Species.Handlers.Queries.CheckExists;
using Species.Application.Species.Handlers.Queries.CheckExists.Queries;
using Species.Application.Species.Handlers.Queries.GetSpecies;
using Species.Application.Species.Handlers.Queries.GetSpecies.Queries;

namespace Species.Application.Extentions.DI;

public static class SpeciesBreedApplicationInjector
{
    public static IServiceCollection AddSpeciesBreedApplication(this IServiceCollection services)
    {
        services.AddQueries();

        services.AddValidatorsFromAssembly(typeof(SpeciesBreedApplicationInjector).Assembly);

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        #region Species
        services.AddScoped<IQueryHandler<PagedList<SpeciesDTO>, GetSpeciesWithPaginationQuery>, GetSpeciesHandler>();
        #endregion

        #region Breed
        services.AddScoped<IQueryHandler<PagedList<BreedDTO>, GetBreedQuery>, GetBreedHandler>();
        #endregion

        #region SpeciesBreed
        services.AddScoped<IQueryHandler<bool, CheckExistsQuery>, CheckExistsHandler>();
        #endregion


        return services;
    }
}
