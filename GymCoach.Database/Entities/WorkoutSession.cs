namespace GymCoach.Database.Entities;

public class WorkoutSession
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public Guid? WorkoutPlanId { get; set; }
    public WorkoutPlan? WorkoutPlan { get; set; }
    public DateTime SessionDate { get; set; }
    public bool IsCompleted { get; set; }
    public ICollection<WorkoutSessionItem> Items { get; set; } = [];
}
