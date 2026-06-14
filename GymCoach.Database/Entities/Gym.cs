using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class Gym
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? LogoUrl { get; set; }
    public decimal CommissionRate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<GymCoachLink> CoachLinks { get; set; } = [];
    public ICollection<GymAthlete> AthleteLinks { get; set; } = [];
    public ICollection<GymAnnouncement> Announcements { get; set; } = [];
}

public class GymCoachLink
{
    public Guid Id { get; set; }
    public Guid GymId { get; set; }
    public Gym Gym { get; set; } = null!;
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}

public class GymAthlete
{
    public Guid Id { get; set; }
    public Guid GymId { get; set; }
    public Gym Gym { get; set; } = null!;
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}

public class GymAnnouncement
{
    public Guid Id { get; set; }
    public Guid GymId { get; set; }
    public Gym Gym { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}

public class PlatformSetting
{
    public Guid Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
