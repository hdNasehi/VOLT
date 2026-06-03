namespace GymCoach.Database.Entities;

public enum ExerciseMediaType
{
    Image = 0,
    Video = 1
}

public class ExerciseMedia
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public ExerciseMediaType MediaType { get; set; }
    public string Url { get; set; } = string.Empty;
    public int Order { get; set; }
}
