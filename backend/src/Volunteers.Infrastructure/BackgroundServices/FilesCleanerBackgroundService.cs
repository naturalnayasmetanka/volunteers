using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Volunteers.Infrastructure.BackgroundServices;

public class FilesCleanerBackgroundService : BackgroundService
{
    ILogger<FilesCleanerBackgroundService> _logger;

    public FilesCleanerBackgroundService(ILogger<FilesCleanerBackgroundService> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
          
        _logger.LogInformation("{0} started", nameof(FilesCleanerBackgroundService));
        throw new NotImplementedException();
    }
}
