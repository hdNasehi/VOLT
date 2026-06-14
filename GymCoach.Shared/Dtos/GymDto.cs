using GymCoach.Shared.Enums;

namespace GymCoach.Shared.Dtos;

public sealed class GymDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? LogoUrl { get; set; }
    public decimal CommissionRate { get; set; }
    public bool IsActive { get; set; }
}

public sealed class CreateGymRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public decimal CommissionRate { get; set; }
}

public sealed class GymAnnouncementDto
{
    public Guid Id { get; set; }
    public Guid GymId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public bool IsActive { get; set; }
}

public sealed class ChatMessageDto
{
    public Guid Id { get; set; }
    public Guid CoachId { get; set; }
    public Guid AthleteId { get; set; }
    public string SenderUserId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class SendMessageRequest
{
    public Guid CoachId { get; set; }
    public Guid AthleteId { get; set; }
    public string Message { get; set; } = string.Empty;
}

public sealed class NotificationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Link { get; set; }
    public bool IsRead { get; set; }
    public NotificationChannel Channel { get; set; }
    public DateTime CreatedAt { get; set; }
}
