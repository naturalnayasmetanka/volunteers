using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.Species.Models;

namespace Volunteers.Infrastructure.Configurations;

public class SpeciesConfigurations : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value)
            );

        builder.Property(x => x.Title)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .IsRequired(true)
            .HasMaxLength(1000);

        builder.HasMany(x => x.Breeds)
            .WithOne(x => x.Species)
            .HasForeignKey("species_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(x => x.Breeds)
            .AutoInclude();
    }
}