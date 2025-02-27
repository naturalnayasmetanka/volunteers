using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Volunteers.Application.Database;

namespace Volunteers.Infrastructure;

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