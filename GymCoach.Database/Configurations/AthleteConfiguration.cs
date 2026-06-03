using GymCoach.Database.Entities;
using GymCoach.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymCoach.Database.Configurations;

public class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
{
    public void Configure(EntityTypeBuilder<Athlete> builder)
    {
        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();

        builder.Property(x => x.UserId)
            .HasMaxLength(450);

        builder.Property(x => x.Email)
            .HasMaxLength(256);

        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.ProfilePhotoUrl).HasMaxLength(500);
    }
}
