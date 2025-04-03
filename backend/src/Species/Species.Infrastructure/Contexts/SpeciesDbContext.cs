using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Species.Infrastructure.Contexts;

public class SpeciesDbContext: DbContext
{
    private const string VOLUNTEERS_CONNECTION_STRING = "VolunteersDbConnectionString";
    private readonly IConfiguration _configuration;

    public SpeciesDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public DbSet<Species> Species { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString(VOLUNTEERS_CONNECTION_STRING));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpeciesDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory() =>
         LoggerFactory.Create(builder => { builder.AddConsole(); });
}
