namespace GymCoach.Database.Entities;

public class ExerciseStep
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int Order { get; set; }
    public string InstructionEn { get; set; } = string.Empty;
    public string InstructionFa { get; set; } = string.Empty;
}
