namespace GymCoach.Shared.Dtos;

public sealed class CoachDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Specialization { get; set; }
}
