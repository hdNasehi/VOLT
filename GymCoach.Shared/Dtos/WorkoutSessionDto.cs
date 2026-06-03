namespace GymCoach.Shared.Dtos;

public sealed class WorkoutSessionDto
{
    public Guid Id { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public Guid AthleteId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
