using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shared.Core.Abstractions.Database;

namespace Shared.Core;

public class SqlConnectionFactory : ISqlConnConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection Create() =>
        new NpgsqlConnection(_configuration.GetConnectionString("VolunteersDbConnectionString"));
}