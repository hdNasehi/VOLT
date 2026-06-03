namespace GymCoach.Database.Entities;

public class ExerciseSupportingMuscle
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
}
