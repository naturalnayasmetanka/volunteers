using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;
using Shared.Core.Models;
using Volunteers.Application.Volunteers.Commands.Create;
using Volunteers.Application.Volunteers.Commands.Create.Commands;
using Volunteers.Application.Volunteers.Commands.Delete;
using Volunteers.Application.Volunteers.Commands.Delete.Commands;
using Volunteers.Application.Volunteers.Commands.Restore;
using Volunteers.Application.Volunteers.Commands.Restore.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites;
using Volunteers.Application.Volunteers.Commands.UpdateRequisites.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks;
using Volunteers.Application.Volunteers.Commands.UpdateSotialNetworks.Commands;
using Volunteers.Application.Volunteers.Queries.GetVolunteer;
using Volunteers.Application.Volunteers.Queries.GetVolunteer.Queries;
using Volunteers.Application.Volunteers.Queries.GetVolunteers;
using Volunteers.Application.Volunteers.Queries.GetVolunteers.Queries;

namespace Volunteers.Application.Extentions.DI;

public static class VolunteersApplicationInjector
{
    public static IServiceCollection AddVolunteersApplication(this IServiceCollection services)
    {
        services.AddVolunteersCommands();
        services.AddVolunteersQueries();

        services.AddValidatorsFromAssembly(typeof(VolunteersApplicationInjector).Assembly);

        return services;
    }

    private static IServiceCollection AddVolunteersCommands(this IServiceCollection services)
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

    private static IServiceCollection AddVolunteersQueries(this IServiceCollection services)
    {
        #region Volunteer
        services.AddScoped<IQueryHandler<PagedList<VolunteerDTO>, GetFilteredWithPaginationVolunteersQuery>, GetPaginateVolunteersHandler>();
        services.AddScoped<IQueryHandler<VolunteerDTO?, GetVolunteerQuery>, GetVolunteerHandler>();
        #endregion

        return services;
    }
}
