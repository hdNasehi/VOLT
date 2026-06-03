using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class Assessment
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public int Age { get; set; }
    public decimal HeightCm { get; set; }
    public decimal WeightKg { get; set; }
    public FitnessGoal Goal { get; set; }
    public TrainingExperience TrainingExperience { get; set; }
    public string? Injuries { get; set; }
    public string? MedicalConditions { get; set; }
    public string? AvailableEquipment { get; set; }
    public int TrainingDaysPerWeek { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
