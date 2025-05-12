namespace Accounts.Infrastructure.Options;

public class JwtOptions
{
    public const string JWT_SECTION_NAME = "JWT";

    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Key { get; set; }
    public int ExpiredTime { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateLifetime { get; set; }
}
