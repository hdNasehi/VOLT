namespace GymCoach.Database.Entities;

public class WorkoutPlanItem
{
    public Guid Id { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public WorkoutPlan WorkoutPlan { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int RestSeconds { get; set; }
    public string? Notes { get; set; }
    public int Order { get; set; }
}
