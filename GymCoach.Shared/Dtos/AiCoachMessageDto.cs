namespace GymCoach.Shared.Dtos;

public sealed class AiCoachMessageDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
