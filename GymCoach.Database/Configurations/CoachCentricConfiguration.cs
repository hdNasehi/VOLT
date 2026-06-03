using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymCoach.Database.Configurations;

public class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder.Property(x => x.HeightCm).HasPrecision(6, 2);
        builder.Property(x => x.WeightKg).HasPrecision(6, 2);
        builder.Property(x => x.Injuries).HasMaxLength(2000);
        builder.Property(x => x.MedicalConditions).HasMaxLength(2000);
        builder.Property(x => x.AvailableEquipment).HasMaxLength(1000);
    }
}

public class ProgressPhotoConfiguration : IEntityTypeConfiguration<ProgressPhoto>
{
    public void Configure(EntityTypeBuilder<ProgressPhoto> builder)
    {
        builder.Property(x => x.PhotoUrl).HasMaxLength(500);
        builder.Property(x => x.Weight).HasPrecision(6, 2);
        builder.Property(x => x.Notes).HasMaxLength(2000);
    }
}

public class CoachNoteConfiguration : IEntityTypeConfiguration<CoachNote>
{
    public void Configure(EntityTypeBuilder<CoachNote> builder)
    {
        builder.Property(x => x.Text).HasMaxLength(4000);

        builder.HasOne(x => x.Coach)
            .WithMany()
            .HasForeignKey(x => x.CoachId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Athlete)
            .WithMany(x => x.CoachNotes)
            .HasForeignKey(x => x.AthleteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ExerciseMediaConfiguration : IEntityTypeConfiguration<ExerciseMedia>
{
    public void Configure(EntityTypeBuilder<ExerciseMedia> builder)
    {
        builder.Property(x => x.Url).HasMaxLength(500);
    }
}

public class ExerciseSupportingMuscleConfiguration : IEntityTypeConfiguration<ExerciseSupportingMuscle>
{
    public void Configure(EntityTypeBuilder<ExerciseSupportingMuscle> builder)
    {
        builder.Property(x => x.NameEn).HasMaxLength(100);
        builder.Property(x => x.NameFa).HasMaxLength(100);
    }
}
