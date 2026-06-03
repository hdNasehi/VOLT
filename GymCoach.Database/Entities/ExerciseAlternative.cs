namespace GymCoach.Database.Entities;

public class ExerciseAlternative
{
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public Guid AlternativeExerciseId { get; set; }
    public Exercise AlternativeExercise { get; set; } = null!;
}
