using GymCoach.Shared.Enums;

namespace GymCoach.Shared.Dtos;

public class CoachDashboardDto
{
    public int TotalAthletes { get; set; }
    public int ActiveAthletes { get; set; }
    public int InactiveOrOnHoldAthletes { get; set; }
    public int NeedsNewProgramCount { get; set; }
    public int NoWorkout7DaysCount { get; set; }
    public IReadOnlyList<CoachAlertDto> AttentionItems { get; set; } = [];
    public IReadOnlyList<ProgramExpiringDto> ProgramsExpiring { get; set; } = [];
    public IReadOnlyList<RecentMeasurementDto> RecentMeasurements { get; set; } = [];
    public IReadOnlyList<RecentProgressPhotoDto> RecentProgressPhotos { get; set; } = [];
}

public class CoachAlertDto
{
    public Guid AthleteId { get; set; }
    public string AthleteNameEn { get; set; } = string.Empty;
    public string AthleteNameFa { get; set; } = string.Empty;
    public string? ProfilePhotoUrl { get; set; }
    public CoachAlertType AlertType { get; set; }
    public CoachAlertSeverity Severity { get; set; }
    public string MessageEn { get; set; } = string.Empty;
    public string MessageFa { get; set; } = string.Empty;
    public DateOnly? RelatedDate { get; set; }
}

public class ProgramExpiringDto
{
    public Guid ProgramId { get; set; }
    public Guid AthleteId { get; set; }
    public string AthleteNameEn { get; set; } = string.Empty;
    public string AthleteNameFa { get; set; } = string.Empty;
    public string ProgramNameEn { get; set; } = string.Empty;
    public string ProgramNameFa { get; set; } = string.Empty;
    public DateOnly EndDate { get; set; }
    public int DaysRemaining { get; set; }
    public bool IsExpired { get; set; }
}

public class RecentMeasurementDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public string AthleteNameEn { get; set; } = string.Empty;
    public string AthleteNameFa { get; set; } = string.Empty;
    public decimal? Weight { get; set; }
    public DateOnly MeasurementDate { get; set; }
}

public class RecentProgressPhotoDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public string AthleteNameEn { get; set; } = string.Empty;
    public string AthleteNameFa { get; set; } = string.Empty;
    public ProgressPhotoCategory Category { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
}
