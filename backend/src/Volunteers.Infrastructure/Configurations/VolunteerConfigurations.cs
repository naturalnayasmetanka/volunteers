using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.PetManagment.Volunteer.AggregateRoot;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.Infrastructure.Configurations;

public class VolunteerConfigurations : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    { 
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value)
            );

        builder.Property(x => x.Name)
            .HasConversion(
                name => name.Value,
                value => Name.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.ExperienceInYears)
            .HasConversion(
                experienceInYears => experienceInYears.Value,
                value => ExperienceInYears.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(4);

        builder.Property(x => x.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value).Value
            )
            .IsRequired(true)
            .HasMaxLength(15);

        builder.HasMany(x => x.Pets)
            .WithOne(x => x.Volunteer)
            .HasForeignKey(x => x.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(x => x.Pets)
            .AutoInclude();

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

        builder.OwnsOne(x => x.SocialNetworkDetails, ib =>
        {
            ib.ToJson();
            ib.OwnsMany(x => x.SocialNetworks, fb =>
            {
                fb.Property(x => x.Title)
                    .IsRequired(true)
                    .HasMaxLength(100);
                fb.Property(x => x.Link)
                    .IsRequired(true)
                    .HasMaxLength(1000);
            });
        });

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        //builder.HasQueryFilter(x => x._isDeleted);
    }
}