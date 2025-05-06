using CSharpFunctionalExtensions;
using Shared.Kernel.CustomErrors;

namespace Shared.Core.Abstractions.Handlers;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<Result<TResponse, Error>> Handle(TQuery query, CancellationToken cancellationToken = default);
}
