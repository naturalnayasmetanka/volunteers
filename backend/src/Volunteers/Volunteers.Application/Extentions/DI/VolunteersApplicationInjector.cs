using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Extentions.DI;

public static class VolunteersApplicationInjector
{
    public static IServiceCollection AddVolunteersApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddQueries();

        services.AddValidatorsFromAssembly(typeof(VolunteersApplicationInjector).Assembly);

        return services;
    }

    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        #region Volunteer 
        services.AddScoped<ICommandHandler<Guid, CreateVolunteerCommand>, CreateVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, SoftDeleteVolunteerCommand>, SoftDeleteVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, HardDeleteVolunteerCommand>, HardDeleteVolunteerHandler>();
        services.AddScoped<ICommandHandler<Guid, RestoreCommand>, RestoreVolunteerHandler>();

        services.AddScoped<ICommandHandler<Guid, UpdateMainInfoCommand>, UpdateMainInfoHandler>();
        services.AddScoped<ICommandHandler<Guid, UpdateSocialNetworksCommand>, UpdateSotialNetworksHandler>();
        services.AddScoped<ICommandHandler<Guid, UpdateRequisiteCommand>, UpdateRequisitesHandler>();
        #endregion

        return services;
    }

    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        #region Volunteer
        services.AddScoped<IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery>, GetPaginateVolunteersHandler>();
        services.AddScoped<IQueryHandler<VolunteerDTO?, GetVolunteerQuery>, GetVolunteerHandler>();
        #endregion

        return services;
    }
}
