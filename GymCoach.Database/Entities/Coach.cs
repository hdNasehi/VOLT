namespace GymCoach.Database.Entities;

public class Coach
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public ICollection<Athlete> Athletes { get; set; } = [];
    public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = [];
}
