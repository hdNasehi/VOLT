namespace GymCoach.Database.Entities;

public class ProgramDay
{
    public Guid Id { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public WorkoutPlan WorkoutPlan { get; set; } = null!;
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public int Order { get; set; }
    public ICollection<ProgramDayExercise> Exercises { get; set; } = [];
}
