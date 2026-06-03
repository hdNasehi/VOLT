namespace GymCoach.Database.Entities;

public class WorkoutSessionItem
{
    public Guid Id { get; set; }
    public Guid WorkoutSessionId { get; set; }
    public WorkoutSession WorkoutSession { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public decimal? Weight { get; set; }
    public int RepsCompleted { get; set; }
    public string? Notes { get; set; }
}
