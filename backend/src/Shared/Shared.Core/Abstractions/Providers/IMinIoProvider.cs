using CSharpFunctionalExtensions;
using Shared.Core.DTO;
using Shared.Kernel.CustomErrors;

namespace Shared.Core.Providers;

public interface IMinIoProvider
{
    Task<Result<List<FileDTO>, List<Error>>> UploadAsync(List<FileDTO> filesData, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetPresignedAsync(FileDTO fileData, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> DeleteAsync(FileDTO fileData, CancellationToken cancellationToken = default);
}
