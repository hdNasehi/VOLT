using GymCoach.Database.Configurations;
using GymCoach.Database.Entities;
using GymCoach.Database.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database;

public sealed class GymCoachDbContext(DbContextOptions<GymCoachDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Coach> Coaches => Set<Coach>();
    public DbSet<Athlete> Athletes => Set<Athlete>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<ExerciseCategory> ExerciseCategories => Set<ExerciseCategory>();
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
    public DbSet<Gym> Gyms => Set<Gym>();
    public DbSet<GymCoachLink> GymCoaches => Set<GymCoachLink>();
    public DbSet<GymAthlete> GymAthletes => Set<GymAthlete>();
    public DbSet<GymAnnouncement> GymAnnouncements => Set<GymAnnouncement>();
    public DbSet<PlatformSetting> PlatformSettings => Set<PlatformSetting>();
    public DbSet<AthleteCoachRequest> AthleteCoachRequests => Set<AthleteCoachRequest>();
    public DbSet<CoachAssessmentRequest> CoachAssessmentRequests => Set<CoachAssessmentRequest>();
    public DbSet<AssessmentPhotoSlot> AssessmentPhotoSlots => Set<AssessmentPhotoSlot>();
    public DbSet<AssessmentPhotoSubmission> AssessmentPhotoSubmissions => Set<AssessmentPhotoSubmission>();
    public DbSet<PaymentOrder> PaymentOrders => Set<PaymentOrder>();
    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();
    public DbSet<CoachEarning> CoachEarnings => Set<CoachEarning>();
    public DbSet<GymCommission> GymCommissions => Set<GymCommission>();
    public DbSet<SettlementBatch> SettlementBatches => Set<SettlementBatch>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<NutritionPlan> NutritionPlans => Set<NutritionPlan>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<MealItem> MealItems => Set<MealItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymCoachDbContext).Assembly);
        modelBuilder.ApplyDecimalPrecision();
    }
}
