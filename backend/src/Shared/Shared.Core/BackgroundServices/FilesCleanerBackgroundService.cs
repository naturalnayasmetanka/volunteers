using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Core.Abstractions.MessageQueues;
using Shared.Core.Abstractions.Providers;
using Shared.Core.DTO;

namespace Shared.Core.BackgroundServices;

public class FilesCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<FilesCleanerBackgroundService> _logger;
    private readonly IMessageQueue<List<FileDTO>> _messageQueue;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FilesCleanerBackgroundService(
        ILogger<FilesCleanerBackgroundService> logger,
        IMessageQueue<List<FileDTO>> messageQueue,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _messageQueue = messageQueue;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateAsyncScope();
        var minIoProvider = scope.ServiceProvider.GetRequiredService<IMinIoProvider>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var fileInfo = await _messageQueue.ReadAsync(stoppingToken);

            if (fileInfo is not null)
                fileInfo.ForEach(async fileData => await minIoProvider.DeleteAsync(fileData: fileData, cancellationToken: stoppingToken));

            _logger.LogInformation("{0} executed", nameof(FilesCleanerBackgroundService));
        }

        await Task.CompletedTask;
    }
}
