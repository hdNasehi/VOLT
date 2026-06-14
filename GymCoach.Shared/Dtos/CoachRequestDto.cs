using GymCoach.Shared.Enums;

namespace GymCoach.Shared.Dtos;

public sealed class AthleteCoachRequestDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Guid CoachId { get; set; }
    public string CoachName { get; set; } = string.Empty;
    public string AthleteName { get; set; } = string.Empty;
    public FitnessGoal Goal { get; set; }
    public TrainingExperience Experience { get; set; }
    public decimal HeightCm { get; set; }
    public decimal WeightKg { get; set; }
    public string? Measurements { get; set; }
    public string? Diseases { get; set; }
    public string? Medications { get; set; }
    public string? Supplements { get; set; }
    public string? Notes { get; set; }
    public RequestStatus Status { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class CreateAthleteCoachRequest
{
    public Guid CoachId { get; set; }
    public FitnessGoal Goal { get; set; }
    public TrainingExperience Experience { get; set; }
    public decimal HeightCm { get; set; }
    public decimal WeightKg { get; set; }
    public string? Measurements { get; set; }
    public string? Diseases { get; set; }
    public string? Medications { get; set; }
    public string? Supplements { get; set; }
    public string? Notes { get; set; }
}

public sealed class RejectRequestDto
{
    public string Reason { get; set; } = string.Empty;
}

public sealed class CoachDetailDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}".Trim();
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public CoachApprovalStatus ApprovalStatus { get; set; }
    public string? Bio { get; set; }
}

public sealed class UpdateCoachApprovalRequest
{
    public CoachApprovalStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}
