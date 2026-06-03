using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class Exercise
{
    public Guid Id { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionFa { get; set; } = string.Empty;
    public MuscleGroup MuscleGroup { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string? Equipment { get; set; }
    public string? VideoUrl { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<ExerciseMuscleActivation> MuscleActivations { get; set; } = [];
    public ICollection<ExerciseSupportingMuscle> SupportingMuscles { get; set; } = [];
    public ICollection<ExerciseMedia> Media { get; set; } = [];
    public ICollection<ExerciseStep> Steps { get; set; } = [];
    public ICollection<ExerciseAlternative> AlternativesFrom { get; set; } = [];
}
