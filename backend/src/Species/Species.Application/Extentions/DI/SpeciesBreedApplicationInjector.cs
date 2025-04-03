using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Species.Application.Extentions.DI;

public static class SpeciesBreedApplicationInjector
{
    public static IServiceCollection AddSpeciesBreedApplication(this IServiceCollection services)
    {
        services.AddQueries();

        services.AddValidatorsFromAssembly(typeof(SpeciesBreedApplicationInjector).Assembly);

        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
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
