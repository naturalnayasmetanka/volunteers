using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Accounts.Infrastructure.Abstractions;
using Accounts.Infrastructure.Models;
using Accounts.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Infrastructure.Providers;

public class JwtTokenProvider : ITokenProvider
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    public JwtTokenProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
        var signingCredentionals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = [
                new Claim("sub", user.Id.ToString()),
                new Claim("exp", DateTime.UtcNow.AddMinutes(_jwtOptions.Value.ExpiredTime).Ticks.ToString())
            ];

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.Value.ExpiredTime),
            signingCredentials: signingCredentionals,
            claims: claims
            );

        var stringToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return stringToken;
    }
}
