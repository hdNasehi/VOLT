using GymCoach.Database.Entities;
using GymCoach.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymCoach.Database.Configurations;

public class CoachAthleteConfiguration : IEntityTypeConfiguration<CoachAthlete>
{
    public void Configure(EntityTypeBuilder<CoachAthlete> builder)
    {
        builder.HasOne(x => x.Coach)
            .WithMany()
            .HasForeignKey(x => x.CoachId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Athlete)
            .WithMany(x => x.CoachLinks)
            .HasForeignKey(x => x.AthleteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.AthleteId, x.Role })
            .IsUnique()
            .HasFilter($"[{nameof(CoachAthlete.IsActive)}] = 1 AND [{nameof(CoachAthlete.Role)}] = {(int)CoachAthleteRole.Fitness}");
    }
}
