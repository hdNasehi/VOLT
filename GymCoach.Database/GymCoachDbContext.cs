using GymCoach.Database.Configurations;
using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database;

public sealed class GymCoachDbContext(DbContextOptions<GymCoachDbContext> options) : DbContext(options)
{
    public DbSet<Coach> Coaches => Set<Coach>();
    public DbSet<Athlete> Athletes => Set<Athlete>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<ExerciseMuscleActivation> ExerciseMuscleActivations => Set<ExerciseMuscleActivation>();
    public DbSet<ExerciseStep> ExerciseSteps => Set<ExerciseStep>();
    public DbSet<ExerciseAlternative> ExerciseAlternatives => Set<ExerciseAlternative>();
    public DbSet<WorkoutPlan> WorkoutPlans => Set<WorkoutPlan>();
    public DbSet<WorkoutPlanItem> WorkoutPlanItems => Set<WorkoutPlanItem>();
    public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();
    public DbSet<WorkoutSessionItem> WorkoutSessionItems => Set<WorkoutSessionItem>();
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public DbSet<PersonalRecord> PersonalRecords => Set<PersonalRecord>();
    public DbSet<CoachAthlete> CoachAthletes => Set<CoachAthlete>();
    public DbSet<Assessment> Assessments => Set<Assessment>();
    public DbSet<ProgressPhoto> ProgressPhotos => Set<ProgressPhoto>();
    public DbSet<CoachNote> CoachNotes => Set<CoachNote>();
    public DbSet<ProgramDay> ProgramDays => Set<ProgramDay>();
    public DbSet<ProgramDayExercise> ProgramDayExercises => Set<ProgramDayExercise>();
    public DbSet<ExerciseSupportingMuscle> ExerciseSupportingMuscles => Set<ExerciseSupportingMuscle>();
    public DbSet<ExerciseMedia> ExerciseMedia => Set<ExerciseMedia>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymCoachDbContext).Assembly);
        modelBuilder.ApplyDecimalPrecision();
        base.OnModelCreating(modelBuilder);
    }
}
