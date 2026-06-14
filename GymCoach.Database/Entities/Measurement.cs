namespace GymCoach.Database.Entities;

public class Measurement
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public decimal? Weight { get; set; }
    public decimal? Chest { get; set; }
    public decimal? Waist { get; set; }
    public decimal? Arms { get; set; }
    public decimal? Forearms { get; set; }
    public decimal? Thighs { get; set; }
    public decimal? Calves { get; set; }
    public decimal? Neck { get; set; }
    public decimal? Shoulders { get; set; }
    public decimal? BodyFatPercentage { get; set; }
    public DateOnly MeasurementDate { get; set; }
}
