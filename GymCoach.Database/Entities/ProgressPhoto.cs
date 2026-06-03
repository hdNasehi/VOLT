using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class ProgressPhoto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public ProgressPhotoCategory Category { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public decimal? Weight { get; set; }
    public string? Notes { get; set; }
}
