using Microsoft.AspNetCore.Mvc;
using Volunteers.Application.Providers;
using Volunteers.Application.Providers.Models;
using Volunteers.Application.Volunteers.AddPet.Commands;

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

        var fileData = new FileData(null, BUCKET_NAME, fileName, expiry);
        var result = await _minioProvider.GetPresignedAsync(fileData, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        IFormFileCollection fileCollection,
        CancellationToken cancellationToken = default)
    {
        List<FileSignature> files = [];
        List<FileData> fileData = [];

        try
        {
            foreach (var file in fileCollection)
            {
                await using var stream = file.OpenReadStream();
                fileData.Add(new FileData(stream, BUCKET_NAME, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)));
            }

            var result = await _minioProvider.UploadAsync(fileData, cancellationToken);

            return Ok(result);
        }
        finally
        {
            foreach (var file in files)
            {
                await file.FileStream.DisposeAsync();
            }
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var fileData = new FileData(null, BUCKET_NAME, fileName);
        var result = await _minioProvider.DeleteAsync(fileData, cancellationToken);
          
        return Ok(result.Value);
    }
}
