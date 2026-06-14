namespace GymCoach.Shared.Dtos;

public sealed class MeasurementRecordDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Chest { get; set; }
    public decimal? Waist { get; set; }
    public decimal? Arms { get; set; }
    public decimal? Thighs { get; set; }
    public decimal? Calves { get; set; }
    public decimal? Neck { get; set; }
    public decimal? BodyFatPercentage { get; set; }
    public DateOnly MeasurementDate { get; set; }
}

public sealed class CreateMeasurementRequest
{
    public decimal? Weight { get; set; }
    public decimal? Chest { get; set; }
    public decimal? Waist { get; set; }
    public decimal? Arms { get; set; }
    public decimal? Thighs { get; set; }
    public decimal? Calves { get; set; }
    public decimal? Neck { get; set; }
    public decimal? BodyFatPercentage { get; set; }
    public DateOnly MeasurementDate { get; set; }
}

public sealed class ProgressPhotoDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;
    public DateOnly DateTaken { get; set; }
    public decimal? Weight { get; set; }
    public string? Notes { get; set; }
    public Enums.ProgressPhotoCategory Category { get; set; }
}

public sealed class CreateProgressPhotoRequest
{
    public string PhotoUrl { get; set; } = string.Empty;
    public DateOnly DateTaken { get; set; }
    public decimal? Weight { get; set; }
    public string? Notes { get; set; }
    public Enums.ProgressPhotoCategory Category { get; set; }
}

public sealed class PersonalRecordDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseNameEn { get; set; } = string.Empty;
    public decimal MaxWeight { get; set; }
    public int MaxReps { get; set; }
    public decimal Estimated1Rm { get; set; }
    public DateOnly AchievedDate { get; set; }
}
