using GymCoach.Shared.Enums;

namespace GymCoach.Shared.Dtos;

public sealed class WorkoutPlanDetailDto
{
    public Guid Id { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionFa { get; set; } = string.Empty;
    public FitnessGoal Goal { get; set; }
    public int DurationWeeks { get; set; }
    public int DaysPerWeek { get; set; }
    public decimal Price { get; set; }
    public ProgramStatus Status { get; set; }
    public Guid AthleteId { get; set; }
    public Guid CoachId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public IReadOnlyList<ProgramDayDto> Days { get; set; } = [];
}

public sealed class ProgramDayDto
{
    public Guid Id { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public int Order { get; set; }
    public IReadOnlyList<ProgramDayExerciseDto> Exercises { get; set; } = [];
}

public sealed class ProgramDayExerciseDto
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string ExerciseNameEn { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int RestSeconds { get; set; }
    public int? SupersetGroupId { get; set; }
    public int Order { get; set; }
}

public sealed class CreateWorkoutPlanRequest
{
    public Guid AthleteId { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionFa { get; set; } = string.Empty;
    public FitnessGoal Goal { get; set; }
    public int DurationWeeks { get; set; }
    public int DaysPerWeek { get; set; }
    public decimal Price { get; set; }
    public DateOnly StartDate { get; set; }
}

public sealed class UpdateWorkoutPlanRequest
{
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionFa { get; set; } = string.Empty;
    public FitnessGoal Goal { get; set; }
    public int DurationWeeks { get; set; }
    public int DaysPerWeek { get; set; }
    public decimal Price { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
