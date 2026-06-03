namespace GymCoach.Shared.Dtos;

public sealed class MeasurementDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public string MetricType { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime RecordedAt { get; set; }
}
