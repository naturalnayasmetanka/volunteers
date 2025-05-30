﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Kernel.Ids;
using Species.Domain.Species.ValueObjects;
using SpeciesModel = Species.Domain.Species.AggregateRoot.Species;

namespace Species.Infrastructure.Configurations;

public class SpeciesConfigurations : IEntityTypeConfiguration<SpeciesModel>
{
    public void Configure(EntityTypeBuilder<SpeciesModel> builder)
    {
        builder.ToTable("species");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value)
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

        builder.HasMany(x => x.Breeds)
            .WithOne(x => x.Species)
            .HasForeignKey("species_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(x => x.Breeds)
            .AutoInclude();
    }
}