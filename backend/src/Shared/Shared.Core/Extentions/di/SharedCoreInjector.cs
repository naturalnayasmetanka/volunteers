using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Serilog;
using Shared.Core.Abstractions.Database;
using Shared.Core.Abstractions.MessageQueues;
using Shared.Core.Abstractions.Providers;
using Shared.Core.BackgroundServices;
using Shared.Core.DTO;
using Shared.Core.MessageQueues;
using Shared.Core.Options;
using Shared.Core.Providers;
using MinIO = Shared.Core.Options.Minio;

namespace Shared.Core.Extentions.DI;

public static class SharedCoreInjector
{
    public static IServiceCollection AddSharedCore(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton<ISqlConnConnectionFactory, SqlConnectionFactory>();

        services.AddLogger(builder);
        services.AddMinIO(builder);

        services.AddMessageQueues(builder);

        services.AddBackgroundServices(builder);

        return services;
    }

    private static IServiceCollection AddLogger(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<Seq>(builder.Configuration.GetSection(Seq.SEQ_SECTION_NAME));  // for DI usage

        var seqOptions = builder.Configuration.GetSection(Seq.SEQ_SECTION_NAME).Get<Seq>()
                ?? throw new ApplicationException("Seq Options not found in appsettings");

        Log.Logger = new LoggerConfiguration()
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

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHostedService<FilesCleanerBackgroundService>();

        return services;
    }

    private static IServiceCollection AddMessageQueues(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton<IMessageQueue<List<FileDTO>>, FilesCleanerMessageQueue>();

        return services;
    }
}
