using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Volunteers.Domain.Species.Models;
using Volunteers.Domain.Volunteer.Models;

namespace Volunteers.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private const string VOLUNTEERS_CONNECTION_STRING = "VolunteersDbConnectionString";
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Species> Species { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(VOLUNTEERS_CONNECTION_STRING));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        private ILoggerFactory CreateLoggerFactory() =>
             LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
