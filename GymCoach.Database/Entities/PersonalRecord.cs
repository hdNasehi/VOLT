namespace GymCoach.Database.Entities;

public class PersonalRecord
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public decimal MaxWeight { get; set; }
    public int MaxReps { get; set; }
    public decimal Estimated1Rm { get; set; }
    public DateOnly AchievedDate { get; set; }

    public static decimal CalculateEstimated1Rm(decimal weight, int reps)
        => reps <= 0 ? weight : weight * (1 + reps / 30m);
}
