using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteers.Domain.Pet.Models;

namespace Volunteers.Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
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
        }
    }
}
