using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class Coach
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Bio { get; set; }
    public CoachApprovalStatus ApprovalStatus { get; set; } = CoachApprovalStatus.Approved;
    public decimal? MaxPlanPrice { get; set; }
    public ICollection<Athlete> Athletes { get; set; } = [];
    public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = [];
    public ICollection<GymCoachLink> GymLinks { get; set; } = [];
}
