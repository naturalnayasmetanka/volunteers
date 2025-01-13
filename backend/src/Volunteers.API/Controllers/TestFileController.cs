using Microsoft.AspNetCore.Mvc;
using Volunteers.Application.DTO;
using Volunteers.Application.Providers;

namespace Volunteers.API.Controllers;

[Route("api/test-file")]
[ApiController]
public class TestFileController : ControllerBase
{
    private const string BUCKET_NAME = "photos";
    private readonly IMinIoProvider _minioProvider;

    public TestFileController(IMinIoProvider minioProvider)
    {
        _minioProvider = minioProvider;
    }

    [HttpGet("presigned")]
    public async Task<IActionResult> Get(
        string fileName,
        int expiry = 60 * 60 * 24,
        CancellationToken cancellationToken = default)
    {
        var fileData = new FileDTO(
            Stream: null,
            BucketName: BUCKET_NAME,
            FileName: fileName,
            ContentType: null,
            Expiry: expiry);

        var result = await _minioProvider.GetPresignedAsync(fileData, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        IFormFileCollection fileCollection,
        CancellationToken cancellationToken = default)
    {
        List<FileDTO> fileData = [];

        try
        {
            foreach (var file in fileCollection)
            {       
                await using var stream = file.OpenReadStream();
                fileData.Add(new FileDTO(
                    Stream: stream,
                    BucketName: BUCKET_NAME,
                    FileName: Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                    ContentType: null));
            }

            var result = await _minioProvider.UploadAsync(fileData, cancellationToken);

            return Ok(result);
        }
        finally
        {
            foreach (var file in fileData)
            {
                if (file.Stream is not null)
                    await file.Stream.DisposeAsync();
            }
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var fileData = new FileDTO(
            Stream: null,
            BucketName: BUCKET_NAME,
            FileName: fileName,
            ContentType: null);

        var result = await _minioProvider.DeleteAsync(fileData, cancellationToken);

        return Ok(result.Value);
    }
}
