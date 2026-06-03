using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymCoach.Database.Configurations;

public class ProgramDayConfiguration : IEntityTypeConfiguration<ProgramDay>
{
    public void Configure(EntityTypeBuilder<ProgramDay> builder)
    {
        builder.HasOne(x => x.WorkoutPlan)
            .WithMany(x => x.Days)
            .HasForeignKey(x => x.WorkoutPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.NameEn).HasMaxLength(200);
        builder.Property(x => x.NameFa).HasMaxLength(200);
    }
}

public class ProgramDayExerciseConfiguration : IEntityTypeConfiguration<ProgramDayExercise>
{
    public void Configure(EntityTypeBuilder<ProgramDayExercise> builder)
    {
        builder.HasOne(x => x.ProgramDay)
            .WithMany(x => x.Exercises)
            .HasForeignKey(x => x.ProgramDayId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Exercise)
            .WithMany()
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.RirRpe).HasMaxLength(20);
        builder.Property(x => x.CoachNotes).HasMaxLength(2000);
    }
}
