using Accounts.Infrastructure.Models;

namespace Accounts.Infrastructure.Abstractions;

public interface ITokenProvider
{
    Task<string> GenerateAccessToken(User user, CancellationToken cancellationToken = default);
}
