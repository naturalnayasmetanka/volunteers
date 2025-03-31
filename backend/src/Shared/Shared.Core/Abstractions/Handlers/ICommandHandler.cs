using CSharpFunctionalExtensions;
using Shared.Kernel.CustomErrors;

namespace Shared.Core.Abstractions.Handlers;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    public Task<Result<TResponse, Error>> Handle(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task Handle(TCommand command, CancellationToken cancellationToken = default);
}
