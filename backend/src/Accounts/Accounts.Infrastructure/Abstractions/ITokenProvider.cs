using Accounts.Infrastructure.Models;

namespace Accounts.Infrastructure.Abstractions;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}
