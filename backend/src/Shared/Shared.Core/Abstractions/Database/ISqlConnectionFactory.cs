using System.Data;

namespace Shared.Core.Abstractions.Database;

public interface ISqlConnConnectionFactory
{
    IDbConnection Create();
}
