using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.Shared.Ids;
using Volunteers.Domain.SpeciesManagment.Breed.Entities;
using Volunteers.Domain.SpeciesManagment.Breed.ValueObjects;

namespace Volunteers.Infrastructure.Configurations;

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