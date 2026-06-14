using GymCoach.Shared.Enums;

namespace GymCoach.Database.Entities;

public class PaymentOrder
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public Guid WorkoutPlanId { get; set; }
    public WorkoutPlan WorkoutPlan { get; set; } = null!;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? PaidAt { get; set; }
    public ICollection<PaymentTransaction> Transactions { get; set; } = [];
}

public class PaymentTransaction
{
    public Guid Id { get; set; }
    public Guid PaymentOrderId { get; set; }
    public PaymentOrder PaymentOrder { get; set; } = null!;
    public string GatewayRef { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class CoachEarning
{
    public Guid Id { get; set; }
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; } = null!;
    public Guid PaymentOrderId { get; set; }
    public PaymentOrder PaymentOrder { get; set; } = null!;
    public decimal Amount { get; set; }
    public Guid? SettlementBatchId { get; set; }
    public SettlementBatch? SettlementBatch { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class GymCommission
{
    public Guid Id { get; set; }
    public Guid GymId { get; set; }
    public Gym Gym { get; set; } = null!;
    public Guid PaymentOrderId { get; set; }
    public PaymentOrder PaymentOrder { get; set; } = null!;
    public decimal Amount { get; set; }
    public Guid? SettlementBatchId { get; set; }
    public SettlementBatch? SettlementBatch { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class SettlementBatch
{
    public Guid Id { get; set; }
    public SettlementType Type { get; set; }
    public SettlementBatchStatus Status { get; set; } = SettlementBatchStatus.Pending;
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public ICollection<CoachEarning> CoachEarnings { get; set; } = [];
    public ICollection<GymCommission> GymCommissions { get; set; } = [];
}
