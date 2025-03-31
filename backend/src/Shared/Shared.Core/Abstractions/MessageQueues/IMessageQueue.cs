namespace Shared.Core.Abstractions.MessageQueues;

public interface IMessageQueue<TMessage>
{
    Task WriteAsync(TMessage messageInfo, CancellationToken cancellationToken = default);
    Task<TMessage> ReadAsync(CancellationToken cancellationToken = default);
}