namespace Volunteers.Infrastructure.Options;

public class Minio
{
    public const string MINIO_SECTION_NAME = "Minio";
    public string Enpdoint { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public bool IsWithSSL { get; set; }
}