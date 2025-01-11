namespace Volunteers.Application.Providers.Models;

public record FileData(
    Stream? Stream,
    string BucketName,
    string FileName,
    int Expiry = 0);