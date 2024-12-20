﻿using CSharpFunctionalExtensions;
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
    private const int MAX_COUNT_FILE_TASKS = 5;

    private List<Error> _errors = [];
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinIoProvider> _logger;

    public MinIoProvider(IMinioClient minioClient, ILogger<MinIoProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<List<string>, List<Error>>> UploadAsync(
        List<FileData> filesData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var semaphoreSlim = new SemaphoreSlim(MAX_COUNT_FILE_TASKS);

            List<string> urls = [];

            if (filesData.Any())
            {
                List<Task> fileTasks = [];

                foreach (var fileData in filesData)
                {
                    await semaphoreSlim.WaitAsync(cancellationToken);

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

                        var task = _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

                        semaphoreSlim.Release();

                        fileTasks.Add(task);

                        _logger.LogInformation($"MINIO:File {fileData.FileName} was added to busket {fileData.BucketName}");
                    }
                    else
                    {
                        _errors.Add(Error.Failure("MINIO: Stream is null", "file.upload"));
                    }
                }

                await Task.WhenAll(fileTasks);
            }
            else
            {
                _errors.Add(Error.Failure("MINIO: There aren`t any files", "file.upload"));
            }

            if (_errors.Any())
            {
                return _errors;
            }

            return urls;
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, $"MINIO:{ex.GetType().Name}:Fail to upload file in bucket.");
            _errors.Add(Error.Failure($"MINIO:{ex.GetType().Name}:Fail to upload file in bucket.", "file.upload"));

            return _errors;
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