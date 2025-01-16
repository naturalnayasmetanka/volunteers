using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Volunteers.Application.Volunteer;
using Volunteers.Infrastructure.Contexts;
using Volunteers.Infrastructure.Repositories;
using Serilog;
using MinIO = Volunteers.Infrastructure.Options.Minio;
using Volunteers.Infrastructure.Options;
using Volunteers.Application.Providers;
using Volunteers.Infrastructure.Providers;
using Volunteers.Application.Database;

namespace Volunteers.Infrastructure.Extentions.DI;

public static class InfractructureInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddLogger(builder);
        services.AddMinIO(builder);
        services.AddSerilog();

        return services;
    }

    private static IServiceCollection AddLogger(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<Seq>(builder.Configuration.GetSection(Seq.SEQ_SECTION_NAME));  // for DI usage

        var seqOptions = builder.Configuration.GetSection(Seq.SEQ_SECTION_NAME).Get<Seq>()
                ?? throw new ApplicationException("Seq Options not found in appsettings");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.Seq(seqOptions.Url ?? throw new ApplicationException("Seq connection not found in appsettings"))
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Roating", Serilog.Events.LogEventLevel.Warning)
            .CreateLogger();

        return services;
    }

    private static IServiceCollection AddMinIO(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<MinIO>(builder.Configuration.GetSection(MinIO.MINIO_SECTION_NAME)); // for DI usage

        services.AddMinio(options =>
        {
            var minioOptions = builder.Configuration.GetSection(MinIO.MINIO_SECTION_NAME).Get<MinIO>()
                ?? throw new ApplicationException("Minio Options not found in appsettings");

            options.WithEndpoint(minioOptions.Enpdoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.IsWithSSL);
            options.Build();
        });

        services.AddScoped<IMinIoProvider, MinIoProvider>();

        return services;
    }
}