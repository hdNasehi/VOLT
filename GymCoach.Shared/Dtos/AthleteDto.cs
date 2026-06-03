using GymCoach.Shared.Enums;

namespace GymCoach.Shared.Dtos;

public sealed class AthleteDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string FullNameFa { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public AthleteStatus Status { get; set; }
    public FitnessGoal Goal { get; set; }
    public decimal? WeightKg { get; set; }
    public DateOnly? LastWorkoutDate { get; set; }
    public int? ProgramProgressPercent { get; set; }
    public bool IsPlaceholder => Status == AthleteStatus.Placeholder;
}
