using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.Volunteer.Models;

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
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.NoAction);

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
    }
}