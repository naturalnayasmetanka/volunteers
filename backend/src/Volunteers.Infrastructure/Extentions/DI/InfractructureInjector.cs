using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Volunteers.Infrastructure.Contexts;
using Volunteers.Infrastructure.Repositories;
using Serilog;
using MinIO = Volunteers.Infrastructure.Options.Minio;
using Volunteers.Infrastructure.Options;
using Volunteers.Application.Providers;
using Volunteers.Infrastructure.Providers;
using Volunteers.Application.Database;
using Volunteers.Infrastructure.BackgroundServices;
using Volunteers.Application.MessageQueues;
using Volunteers.Infrastructure.MessageQueues;
using Volunteers.Application.DTO;
using Volunteers.Application.Handlers.Volunteers;

namespace Volunteers.Infrastructure.Extentions.DI;

public static class InfractructureInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ISqlConnConnectionFactory, SqlConnectionFactory>();

        services.AddLogger(builder);
        services.AddMinIO(builder);
        services.AddSerilog();

        services.AddMessageQueues(builder);

        services.AddBackgroundServices(builder);

        return services;
    }
}