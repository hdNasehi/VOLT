using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class ChatMessage
{
    public Guid Id { get; set; }
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public string SenderUserId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Notification
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Link { get; set; }
    public bool IsRead { get; set; }
    public NotificationChannel Channel { get; set; } = NotificationChannel.InApp;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class NutritionPlan
{
    public Guid Id { get; set; }
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Meal> Meals { get; set; } = [];
}

public class Meal
{
    public Guid Id { get; set; }
    public Guid NutritionPlanId { get; set; }
    public NutritionPlan NutritionPlan { get; set; } = null!;
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public int Order { get; set; }
    public ICollection<MealItem> Items { get; set; } = [];
}

public class MealItem
{
    public Guid Id { get; set; }
    public Guid MealId { get; set; }
    public Meal Meal { get; set; } = null!;
    public string NameEn { get; set; } = string.Empty;
    public string NameFa { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int Order { get; set; }
}
