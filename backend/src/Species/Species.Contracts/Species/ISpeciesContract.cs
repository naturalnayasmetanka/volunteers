using CSharpFunctionalExtensions;
using Shared.Kernel.CustomErrors;
using Species.Contracts.Species.Requests.Species.CheckExists;

namespace Species.Contracts.Species;

public interface ISpeciesContract
{
    Task<Result<bool, Error>> CheckExists(CheckExistsRequest request, CancellationToken cancellationToken = default);
}
