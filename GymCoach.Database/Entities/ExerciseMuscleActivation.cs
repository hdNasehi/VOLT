namespace GymCoach.Database.Entities;

public class ExerciseMuscleActivation
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public string MuscleNameEn { get; set; } = string.Empty;
    public string MuscleNameFa { get; set; } = string.Empty;
    public int ActivationPercent { get; set; }
}
