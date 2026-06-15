using GymCoach.Shared.Enums;

namespace GymCoach.Client.Client.Models.Athlete;

public enum WorkoutDayState
{
    Future,
    Current,
    Completed,
    Missed
}

public sealed class AthleteDashboardModel
{
    public string AthleteName { get; set; } = string.Empty;
    public string CoachName { get; set; } = string.Empty;
    public string GymName { get; set; } = string.Empty;
    public ActiveProgramModel? ActiveProgram { get; set; }
    public BodyProgressModel BodyProgress { get; set; } = new();
    public CoachMessagePreviewModel CoachMessage { get; set; } = new();
    public PendingRequestsModel PendingRequests { get; set; } = new();
    public IReadOnlyList<ChartPointModel> WeightTrend { get; set; } = [];
    public IReadOnlyList<ChartPointModel> BodyFatTrend { get; set; } = [];
    public IReadOnlyList<ProgramSummaryModel> PreviousPrograms { get; set; } = [];
}

public sealed class ActiveProgramModel
{
    public Guid ProgramId { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public string CoachName { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int CurrentDay { get; set; }
    public int TotalDays { get; set; }
    public int ProgressPercent { get; set; }
    public int DaysRemaining { get; set; }
    public Guid CurrentDayId { get; set; }
}

public sealed class BodyProgressModel
{
    public decimal? WeightKg { get; set; }
    public decimal? BodyFatPercent { get; set; }
    public DateOnly? LastMeasured { get; set; }
}

public sealed class CoachMessagePreviewModel
{
    public string LastMessage { get; set; } = string.Empty;
    public int UnreadCount { get; set; }
    public DateTime SentAt { get; set; }
}

public sealed class PendingRequestsModel
{
    public int PhotoRequests { get; set; }
    public int MeasurementRequests { get; set; }
}

public sealed class ChartPointModel
{
    public string Label { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public sealed class ProgramSummaryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CoachName { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int ProgressPercent { get; set; }
    public ProgramStatus Status { get; set; }
    public bool IsActive => Status == ProgramStatus.Active;
}

public sealed class ProgramDetailModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CoachName { get; set; } = string.Empty;
    public FitnessGoal Goal { get; set; }
    public int DurationWeeks { get; set; }
    public IReadOnlyList<WorkoutDaySummaryModel> Days { get; set; } = [];
}

public sealed class WorkoutDaySummaryModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public WorkoutDayState State { get; set; }
}

public sealed class WorkoutDayDetailModel
{
    public Guid Id { get; set; }
    public Guid ProgramId { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public string DayName { get; set; } = string.Empty;
    public IReadOnlyList<ExerciseDetailModel> Exercises { get; set; } = [];
}

public sealed class ExerciseDetailModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MuscleGroup { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public string? GifUrl { get; set; }
    public IReadOnlyList<string> Tips { get; set; } = [];
    public IReadOnlyList<string> CommonMistakes { get; set; } = [];
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int RestSeconds { get; set; }
    public string? SetScheme { get; set; }
    public string? CoachNotes { get; set; }
    public int? SupersetGroupId { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsLocked { get; set; }
}

public sealed class WorkoutSessionModel
{
    public Guid SessionId { get; set; }
    public Guid DayId { get; set; }
    public string DayName { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public IReadOnlyList<ExerciseDetailModel> Exercises { get; set; } = [];
    public int CurrentExerciseIndex { get; set; }
    public int? RestSecondsRemaining { get; set; }
    public bool IsResting { get; set; }
}

public sealed class PhotoRequestModel
{
    public Guid Id { get; set; }
    public string BodyPart { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public string? ExampleImageUrl { get; set; }
    public string? UploadedImageUrl { get; set; }
    public AssessmentReviewStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}
