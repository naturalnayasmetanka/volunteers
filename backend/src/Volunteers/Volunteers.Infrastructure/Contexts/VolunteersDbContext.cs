using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Volunteers.Domain.Volunteers.AggregateRoot;

namespace Volunteers.Infrastructure.Contexts;

public class VolunteersDbContext : DbContext
{
    private const string VOLUNTEERS_CONNECTION_STRING = "VolunteersDbConnectionString";
    private readonly IConfiguration _configuration;

    public VolunteersDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Volunteer> Volunteers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString(VOLUNTEERS_CONNECTION_STRING));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("volunteers_pets");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VolunteersDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory() =>
         LoggerFactory.Create(builder => { builder.AddConsole(); });
}
