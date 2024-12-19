using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.PetManagment.Pet.Entities;
using Volunteers.Domain.PetManagment.Pet.ValueObjects;
using Volunteers.Domain.Shared.Ids;
using Volunteers.Infrastructure.Extentions.EF;

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

        builder.Property(x => x.VolunteerId)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value)
            );

        builder.Property(x => x.Nickname)
            .HasConversion(
                nickname => nickname.Value,
                value => Nickname.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(20);

        builder.Property(x => x.CommonDescription)
            .HasConversion(
                commonDescription => commonDescription.Value,
                value => CommonDescription.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(1000);

        builder.Property(x => x.HelthDescription)
            .HasConversion(
                helthDescription => helthDescription.Value,
                value => HelthDescription.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(1000);

        builder.Property(x => x.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PetPhoneNumber.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(15);

        builder.Property(x => x.Position)
            .HasConversion(
                position => position.Value,
                value => Position.Create(value).Value
            )
            .IsRequired(true);

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
        }) ;

        builder.OwnsOne(x => x.RequisitesDetails, ib =>
        {
            ib.ToJson();
            ib.OwnsMany(x => x.PetRequisites, fb =>
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
            ib.OwnsMany(x => x.PetPhoto, fb =>
            {
                fb.Property(x => x.Path)
                    .IsRequired(true)
                    .HasMaxLength(1000);
                fb.Property(x => x.IsMain)
                    .IsRequired(true);
            });
        });

        builder.OwnsOne(x => x.PhysicalParametersDetails, ib =>
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

        builder.Property(x => x.SpeciesBreed)
            .JsonValueObjectСonversion();

        builder.Property<bool>("_isDeleted")
           .UsePropertyAccessMode(PropertyAccessMode.Field)
           .HasColumnName("is_deleted");
    }
}