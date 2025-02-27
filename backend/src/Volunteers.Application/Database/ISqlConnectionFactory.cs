using System.Data;

namespace Volunteers.Application.Database;

public interface ISqlConnConnectionFactory
{
    IDbConnection Create();
}
