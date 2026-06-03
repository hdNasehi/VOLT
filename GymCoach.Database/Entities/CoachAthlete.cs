using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class CoachAthlete
{
    public Guid Id { get; set; }
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public CoachAthleteRole Role { get; set; } = CoachAthleteRole.Fitness;
    public bool IsActive { get; set; } = true;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
