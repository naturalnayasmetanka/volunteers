using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpeciesModel = Species.Domain.Species.AggregateRoot.Species;

namespace Species.Infrastructure.Contexts;

public class SpeciesDbContext : DbContext
{
    private const string VOLUNTEERS_CONNECTION_STRING = "VolunteersDbConnectionString";
    private readonly IConfiguration _configuration;

    public SpeciesDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<SpeciesModel> Species { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString(VOLUNTEERS_CONNECTION_STRING));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SpeciesBreed");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpeciesDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory() =>
         LoggerFactory.Create(builder => { builder.AddConsole(); });
}
