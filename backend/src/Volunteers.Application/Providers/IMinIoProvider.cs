using CSharpFunctionalExtensions;
using Volunteers.Application.Providers.Models;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Providers;

public interface IMinIoProvider
{
    Task<Result<string, Error>> UploadAsync(FileData fileData, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetPresignedAsync(FileData fileData, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> DeleteAsync(FileData fileData, CancellationToken cancellationToken = default);
}
