namespace GymCoach.Shared.Dtos;

public sealed class WorkoutPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid AthleteId { get; set; }
    public Guid CoachId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
