namespace GymCoach.Shared.Dtos;

public sealed class ExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MuscleGroup { get; set; } = string.Empty;
    public string? Description { get; set; }
}
