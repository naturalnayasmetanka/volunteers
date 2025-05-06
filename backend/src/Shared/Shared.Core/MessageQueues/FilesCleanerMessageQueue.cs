using Shared.Core.Abstractions.MessageQueues;
using Shared.Core.DTO;
using System.Threading.Channels;

namespace Shared.Core.MessageQueues;

public class FilesCleanerMessageQueue : IMessageQueue<List<FileDTO>>
{
    private readonly Channel<List<FileDTO>> _channel = Channel.CreateUnbounded<List<FileDTO>>();

    public async Task WriteAsync(List<FileDTO> filesInfo, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(filesInfo, cancellationToken);
    }

    public async Task<List<FileDTO>> ReadAsync(CancellationToken cancellationToken = default)
    {
        var readResult = await _channel.Reader.ReadAsync(cancellationToken);

        return readResult;
    }
}
