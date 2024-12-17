using Microsoft.AspNetCore.Mvc;
using Volunteers.Application.Providers;
using Volunteers.Application.Providers.Models;

namespace Volunteers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestFileController : ControllerBase
{
    private readonly IMinIoProvider _minioProvider;

    public TestFileController(IMinIoProvider minioProvider)
    {
        _minioProvider = minioProvider;
    }

    [HttpGet("presigned")]
    public async Task<IActionResult> Get(
        string bucket,
        string fileName,
        CancellationToken cancellationToken = default)
    {

        var fileData = new FileData(null, bucket, fileName, 60 * 60 * 24);
        var result = await _minioProvider.GetPresignedAsync(fileData, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        var fileData = new FileData(stream, "photos", file.FileName);
        var result = await _minioProvider.UploadAsync(fileData, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        string bucket,
        string fileName,
        CancellationToken cancellationToken = default)
    {

        var fileData = new FileData(null, bucket, fileName);
        var result = await _minioProvider.DeleteAsync(fileData, cancellationToken);

        return Ok(result.Value);
    }
}
