using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.Pet.Models;

namespace Volunteers.Infrastructure.Configurations;

public class PetConfigurations : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value)
            );

        builder.Property(x => x.Nickname)
            .IsRequired(true)
            .HasMaxLength(20);

        builder.Property(x => x.CommonDescription)
            .IsRequired(true)
            .HasMaxLength(1000);

        builder.Property(x => x.HelthDescription)
            .IsRequired(true)
            .HasMaxLength(1000);

        builder.Property(x => x.PhoneNumber)
            .IsRequired(true)
            .HasMaxLength(15);

        builder.Property(x => x.HelpStatus)
            .IsRequired(true);

        builder.Property(x => x.BirthDate)
            .IsRequired(false);

        builder.Property(x => x.CreationDate)
            .IsRequired(true);

        builder.OwnsOne(x => x.LocationDetails, ib =>
        {
            ib.ToJson();

            ib.OwnsMany(x => x.Locations, fb =>
            {
                fb.Property(x => x.Country)
                    .IsRequired(true)
                    .HasMaxLength(50);

                fb.Property(x => x.City)
                    .IsRequired(true)
                    .HasMaxLength(50);

                fb.Property(x => x.Street)
                    .IsRequired(true)
                    .HasMaxLength(50);

                fb.Property(x => x.HouseNumber)
                    .IsRequired(true)
                    .HasMaxLength(4);

                fb.Property(x => x.Floor)
                    .IsRequired(true)
                    .HasMaxLength(3);

                fb.Property(x => x.RoomNumber)
                    .IsRequired(true)
                    .HasMaxLength(5);
            });
        });

        builder.OwnsOne(x => x.RequisiteDetails, ib =>
        {
            ib.ToJson();

            ib.OwnsMany(x => x.Requisites, fb =>
            {
                fb.Property(x => x.Title)
                    .IsRequired(true)
                    .HasMaxLength(100);

                fb.Property(x => x.Description)
                    .IsRequired(true)
                    .HasMaxLength(1000);
            });
        });

        builder.OwnsOne(x => x.PhotoDetails, ib =>
        {
            ib.ToJson();

            ib.OwnsMany(x => x.Photo, fb =>
            {
                fb.Property(x => x.Path)
                    .IsRequired(true)
                    .HasMaxLength(1000);

                fb.Property(x => x.IsMain)
                    .IsRequired(true);
            });
        });

        builder.OwnsOne(x => x.PhysicalDetails, ib =>
        {
            ib.ToJson();

            ib.OwnsMany(x => x.PhysicalParameters, fb =>
            {
                fb.Property(x => x.Type)
                    .IsRequired(true)
                    .HasMaxLength(50);

                fb.Property(x => x.Gender)
                    .IsRequired(false)
                    .HasMaxLength(20);

                fb.Property(x => x.Breed)
                    .IsRequired(false)
                    .HasMaxLength(100);

                fb.Property(x => x.Color)
                    .IsRequired(false)
                    .HasMaxLength(100);

                fb.Property(x => x.Weight)
                    .IsRequired(true)
                    .HasMaxLength(5);

                fb.Property(x => x.Height)
                    .IsRequired(true)
                    .HasMaxLength(5);

                fb.Property(x => x.IsVaccinated)
                    .IsRequired(true);

                fb.Property(x => x.IsSterilized)
                    .IsRequired(true);
            });
        });
    }
}