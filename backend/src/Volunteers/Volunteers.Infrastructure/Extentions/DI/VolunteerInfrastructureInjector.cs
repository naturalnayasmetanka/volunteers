using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions.Database;
using Shared.Core.Enums;
using Volunteers.Application.Volunteers;
using Volunteers.Infrastructure.Contexts;
using Volunteers.Infrastructure.Repositories;

namespace Volunteers.Infrastructure.Extentions.DI;

public static class VolunteerInfrastructureInjector
{
    public static IServiceCollection AddVolunteerInfrastructure(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, VolunteersUnitOfWork>(UoWServiceDI.VolunteerService);
        services.AddScoped<VolunteersDbContext>();

        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }
}
