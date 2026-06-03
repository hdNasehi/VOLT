namespace GymCoach.Database.Entities;

public class ProgramDayExercise
{
    public Guid Id { get; set; }
    public Guid ProgramDayId { get; set; }
    public ProgramDay ProgramDay { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int RestSeconds { get; set; }
    public string? RirRpe { get; set; }
    public string? CoachNotes { get; set; }
    public int Order { get; set; }
}
