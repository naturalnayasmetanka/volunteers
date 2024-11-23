using Microsoft.Extensions.DependencyInjection;
using Volunteers.Application.Volunteer;
using Volunteers.Infrastructure.Contexts;
using Volunteers.Infrastructure.Repositories;

namespace Volunteers.Infrastructure.Extentions.DI;

public static class InfractructureInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }
}