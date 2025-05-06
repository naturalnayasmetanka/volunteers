using CSharpFunctionalExtensions;
using Shared.Core.Abstractions.Handlers;
using Shared.Kernel.CustomErrors;
using Species.Application.Species.Handlers.Queries.CheckExists.Queries;
using Species.Contracts.Species;
using Species.Contracts.Species.Requests.Species.CheckExists;

namespace Species.Presentation.ContractsEmplementations;

public class SpeciesContract : ISpeciesContract
{
    IQueryHandler<bool, CheckExistsQuery> _handler;
    public SpeciesContract(IQueryHandler<bool, CheckExistsQuery> handler)
    {
        _handler = handler;
    }

    public async Task<Result<bool, Error>> CheckExists(CheckExistsRequest request, CancellationToken cancellationToken = default)
    {
        var checkExistSpeciesBreedQuery = new CheckExistsQuery(SpeciesId: request.SpeciesId, BreedId: request.BreedId);

        return await _handler.Handle(checkExistSpeciesBreedQuery, cancellationToken);
    }
}
