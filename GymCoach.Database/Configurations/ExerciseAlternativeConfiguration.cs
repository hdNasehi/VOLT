using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymCoach.Database.Configurations;

public class ExerciseAlternativeConfiguration : IEntityTypeConfiguration<ExerciseAlternative>
{
    public void Configure(EntityTypeBuilder<ExerciseAlternative> builder)
    {
        builder.HasKey(x => new { x.ExerciseId, x.AlternativeExerciseId });
        builder.HasOne(x => x.Exercise)
            .WithMany(x => x.AlternativesFrom)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.AlternativeExercise)
            .WithMany()
            .HasForeignKey(x => x.AlternativeExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
