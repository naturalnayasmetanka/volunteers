using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.Volunteer.Models;
using Volunteers.Infrastructure.Extentions.EF;

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
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(x => x.ExperienceInYears)
            .IsRequired(true)
            .HasMaxLength(4);

        builder.Property(x => x.PhoneNumber)
            .IsRequired(true)
            .HasMaxLength(15);

        builder.HasMany(x => x.Pets)
            .WithOne(x => x.Volunteer)
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(x => x.Pets)
            .AutoInclude();

        builder.Property(x => x.SocialNetworks)
            .JsonValueObjectCollectionСonversion();

        builder.Property(x => x.Requisites)
            .JsonValueObjectCollectionСonversion();
    }
}