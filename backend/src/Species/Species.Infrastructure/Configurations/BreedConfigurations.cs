using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Kernel.Ids;
using Species.Domain.Breeds.Entities;
using Species.Domain.Breeds.ValueObjects;

namespace Species.Infrastructure.Configurations;

public class BreedConfigurations : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value)
            );

        builder.Property(x => x.Title)
            .HasConversion(
                title => title.Value,
                value => Title.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasConversion(
                description => description.Value,
                value => Description.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(1000);
    }
}