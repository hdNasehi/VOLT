namespace GymCoach.Database.Entities;

public class CoachNote
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
