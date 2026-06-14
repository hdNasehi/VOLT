using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class AthleteCoachRequest
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public FitnessGoal Goal { get; set; }
    public TrainingExperience Experience { get; set; }
    public decimal HeightCm { get; set; }
    public decimal WeightKg { get; set; }
    public string? Measurements { get; set; }
    public string? Diseases { get; set; }
    public string? Medications { get; set; }
    public string? Supplements { get; set; }
    public string? Notes { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? RespondedAt { get; set; }
}

public class CoachAssessmentRequest
{
    public Guid Id { get; set; }
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public AssessmentReviewStatus Status { get; set; } = AssessmentReviewStatus.Pending;
    public string? CoachNotes { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedAt { get; set; }
    public ICollection<AssessmentPhotoSlot> PhotoSlots { get; set; } = [];
}

public class AssessmentPhotoSlot
{
    public Guid Id { get; set; }
    public Guid AssessmentRequestId { get; set; }
    public CoachAssessmentRequest AssessmentRequest { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? SampleImageUrl { get; set; }
    public string? Instructions { get; set; }
    public int Order { get; set; }
    public ICollection<AssessmentPhotoSubmission> Submissions { get; set; } = [];
}

public class AssessmentPhotoSubmission
{
    public Guid Id { get; set; }
    public Guid PhotoSlotId { get; set; }
    public AssessmentPhotoSlot PhotoSlot { get; set; } = null!;
    public string ImageUrl { get; set; } = string.Empty;
    public AssessmentReviewStatus Status { get; set; } = AssessmentReviewStatus.Pending;
    public string? RejectionReason { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedAt { get; set; }
}

public class ExerciseCategory
{
    public Guid Id { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public int Order { get; set; }
    public ICollection<Exercise> Exercises { get; set; } = [];
}
