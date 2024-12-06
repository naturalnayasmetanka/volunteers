using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Volunteers.Application.Volunteer;
using Volunteers.Infrastructure.Contexts;
using Volunteers.Infrastructure.Repositories;
using MinIO = Volunteers.Infrastructure.Options.Minio;

namespace Volunteers.Infrastructure.Extentions.DI;

public static class InfractructureInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        services.AddMinIO(builder);

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
        });

        return services;
    }
}