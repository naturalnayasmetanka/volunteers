using CSharpFunctionalExtensions;
using Volunteers.Application.DTO;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Application.Providers;

public interface IMinIoProvider
{
    Task<Result<List<string>, List<Error>>> UploadAsync(List<FileDTO> filesData, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetPresignedAsync(FileDTO fileData, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> DeleteAsync(FileDTO fileData, CancellationToken cancellationToken = default);
}
