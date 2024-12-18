using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Volunteers.Application.Providers;
using Volunteers.Application.Providers.Models;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Infrastructure.Providers;

public class MinIoProvider : IMinIoProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinIoProvider> _logger;

    public MinIoProvider(IMinioClient minioClient, ILogger<MinIoProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, Error>> UploadAsync(
        FileData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (fileData.Stream != null)
            {
                var bucket = new BucketExistsArgs()
                    .WithBucket(fileData.BucketName);

                var bucketExist = await _minioClient
                    .BucketExistsAsync(bucket, cancellationToken);

                if (!bucketExist)
                {
                    var makeBucket = new MakeBucketArgs()
                        .WithBucket(fileData.BucketName);

                    await _minioClient.MakeBucketAsync(makeBucket, cancellationToken);
                }

                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(fileData.BucketName)
                    .WithStreamData(fileData.Stream)
                    .WithObjectSize(fileData.Stream.Length)
                    .WithObject(Guid.NewGuid().ToString() + "_" + fileData.FileName);

                var result = await _minioClient
                    .PutObjectAsync(putObjectArgs, cancellationToken);

                _logger.LogInformation($"MINIO:File {fileData.FileName} was added to busket {fileData.BucketName}");

                return result.ObjectName;
            }
            else
            {
                return Error.Failure("MINIO: Stream is null", "file.upload");
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"MINIO:{ex.GetType().Name}:Fail to upload file {fileData.FileName} in bucket {fileData.BucketName}.");

            return Error.Failure($"MINIO:{ex.GetType().Name}:Fail to upload file {fileData.FileName} in bucket {fileData.BucketName}.", "file.upload");
        }
    }

    public async Task<Result<string, Error>> GetPresignedAsync(
        FileData fileData,
        CancellationToken cancellationToken)
    {
        try
        {
            var statArgs = new StatObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.FileName);

            var statObject = await _minioClient.StatObjectAsync(statArgs, cancellationToken);

            if (statObject is null)
            {
                throw new InvalidObjectNameException($"Object {fileData.FileName} was not found");
            }

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

    public async Task<Result<string, Error>> DeleteAsync(FileData fileData, CancellationToken cancellationToken)
    {
        try
        {
            var statArgs = new StatObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.FileName);

            var statObject = await _minioClient.StatObjectAsync(statArgs, cancellationToken);

            if (statObject is null)
            {
                throw new InvalidObjectNameException($"Object {fileData.FileName} was not found");
            }

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
