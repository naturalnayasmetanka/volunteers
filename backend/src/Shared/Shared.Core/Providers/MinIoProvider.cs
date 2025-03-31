using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Shared.Core.DTO;
using Shared.Kernel.CustomErrors;

namespace Shared.Core.Providers;

public class MinIoProvider : IMinIoProvider
{
    private const int MAX_COUNT_FILE_TASKS = 5;

    private List<Error> _errors = [];
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinIoProvider> _logger;

    public MinIoProvider(IMinioClient minioClient, ILogger<MinIoProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<List<FileDTO>, List<Error>>> UploadAsync(
        List<FileDTO> filesData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_COUNT_FILE_TASKS);

        List<string> urls = [];

        try
        {
            if (filesData.Any())
            {
                await MakeBucketIsItNull(filesData, cancellationToken);

                var tasksResult = filesData.Select(async file =>
                    await PutObject(file, semaphoreSlim, cancellationToken));

                var pathsResult = await Task.WhenAll(tasksResult);
                urls = pathsResult.Select(p => p.Value).ToList();
            }
            else
            {
                _errors.Add(Error.Failure("MINIO: There aren`t any files", "file.upload"));
            }

            if (_errors.Any())
                return _errors;

            return filesData;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MINIO:{ex.GetType().Name}:Fail to upload file in bucket.");
            _errors.Add(Error.Failure($"MINIO:{ex.GetType().Name}:Fail to upload file in bucket.", "file.upload"));

            return _errors;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task<Result<string, List<Error>>> PutObject(
        FileDTO fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            if (fileData.Stream != null)
            {
                var putObjectArgs = new PutObjectArgs()
                            .WithBucket(fileData.BucketName)
                            .WithStreamData(fileData.Stream)
                            .WithObjectSize(fileData.Stream.Length)
                            .WithObject(fileData.FileName);

                await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

                _logger.LogInformation($"MINIO:File {fileData.FileName} was added to busket {fileData.BucketName}");

                return fileData.FileName;
            }
            else
            {
                _logger.LogError("MINIO: Stream is null");
                _errors.Add(Error.Failure("MINIO: Stream is null", "file.upload"));

                return _errors;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FileName,
                fileData.BucketName);

            return _errors;
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task MakeBucketIsItNull(
        List<FileDTO> filesData,
        CancellationToken cancellationToken = default)
    {
        HashSet<string> bucketNames = [.. filesData.Select(file => file.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    public async Task<Result<string, Error>> GetPresignedAsync(
        FileDTO fileData,
        CancellationToken cancellationToken)
    {
        try
        {
            var statArgs = new StatObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.FileName);

            var statObject = await _minioClient.StatObjectAsync(statArgs, cancellationToken);

            if (statObject is null)
                throw new InvalidObjectNameException($"Object {fileData.FileName} was not found");

            var presignetArgs = new PresignedGetObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.FileName)
                .WithExpiry(fileData.Expiry);

            var url = await _minioClient.PresignedGetObjectAsync(presignetArgs);

            return url;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MINIO:{ex.GetType().Name}:Fail to get Presigned {fileData.FileName} in bucket {fileData.BucketName}.");

            return Error.Failure($"MINIO:{ex.GetType().Name}:Fail to get Presigned {fileData.FileName} in bucket {fileData.BucketName}.", "presigned.get");
        }
    }

    public async Task<Result<string, Error>> DeleteAsync(FileDTO fileData, CancellationToken cancellationToken)
    {
        try
        {
            var statArgs = new StatObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.FileName);

            var statObject = await _minioClient.StatObjectAsync(statArgs, cancellationToken);

            if (statObject is null)
                throw new InvalidObjectNameException($"Object {fileData.FileName} was not found");

            var presignetArgs = new RemoveObjectArgs()
                       .WithBucket(fileData.BucketName)
                       .WithObject(fileData.FileName);

            await _minioClient.RemoveObjectAsync(presignetArgs, cancellationToken);

            return fileData.FileName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MINIO:{ex.GetType().Name}:Fail to remove {fileData.FileName} in bucket {fileData.BucketName}.");

            return Error.Failure($"MINIO:{ex.GetType().Name}:Fail to to remove {fileData.FileName} in bucket {fileData.BucketName}.", "file.remove");
        }
    }
}
