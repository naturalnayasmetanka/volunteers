using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.Breed.Models;

namespace Volunteers.Infrastructure.Configurations;

public class BreedConfigurations : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value)
            );

        builder.Property(x => x.Title)
               .IsRequired(true)
               .HasMaxLength(50);

        builder.Property(x => x.Description)
            .IsRequired(true)
            .HasMaxLength(1000);
    }
}