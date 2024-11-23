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

        builder.Property(x => x.HelpStatus)
            .IsRequired(true);

        builder.Property(x => x.BirthDate)
            .IsRequired(false);

        builder.Property(x => x.CreationDate)
            .IsRequired(true);

        builder.Property(x => x.Locations)
            .JsonValueObjectCollectionСonversion();

        builder.Property(x => x.Requisites)
            .JsonValueObjectCollectionСonversion();

        builder.Property(x => x.Photo)
            .JsonValueObjectCollectionСonversion();

        builder.Property(x => x.PhysicalParameters)
            .JsonValueObjectCollectionСonversion();

        builder.Property(x => x.SpeciesBreed)
            .JsonValueObjectСonversion();
    }
}