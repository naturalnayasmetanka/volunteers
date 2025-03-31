using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Abstractions;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    public Task<Result<TResponse, Error>> Handle(TCommand command, CancellationToken cancellationToken = default);
}



public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task Handle(TCommand command, CancellationToken cancellationToken = default);
}