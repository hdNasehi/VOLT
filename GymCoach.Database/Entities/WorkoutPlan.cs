using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class WorkoutPlan
{
    public Guid Id { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionFa { get; set; } = string.Empty;
    public FitnessGoal Goal { get; set; }
    public int DurationWeeks { get; set; }
    public ProgramStatus Status { get; set; } = ProgramStatus.Active;
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public ICollection<WorkoutPlanItem> Items { get; set; } = [];
    public ICollection<ProgramDay> Days { get; set; } = [];
}
