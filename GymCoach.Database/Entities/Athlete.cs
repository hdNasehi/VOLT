using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class Athlete
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateOnly? BirthDate { get; set; }
    public decimal? HeightCm { get; set; }
    public decimal? WeightKg { get; set; }
    public decimal? BodyFatPercentage { get; set; }
    public FitnessGoal Goal { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public AthleteStatus Status { get; set; } = AthleteStatus.Active;
    public string? ProfilePhotoUrl { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public int StreakDays { get; set; }
    public ICollection<CoachAthlete> CoachLinks { get; set; } = [];
    public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = [];
    public ICollection<WorkoutSession> WorkoutSessions { get; set; } = [];
    public ICollection<Measurement> Measurements { get; set; } = [];
    public ICollection<PersonalRecord> PersonalRecords { get; set; } = [];
    public ICollection<Assessment> Assessments { get; set; } = [];
    public ICollection<ProgressPhoto> ProgressPhotos { get; set; } = [];
    public ICollection<CoachNote> CoachNotes { get; set; } = [];
}
