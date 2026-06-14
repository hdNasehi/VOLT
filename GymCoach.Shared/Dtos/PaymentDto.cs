using GymCoach.Shared.Enums;

namespace GymCoach.Shared.Dtos;

public sealed class PaymentOrderDto
{
    public Guid Id { get; set; }
    public Guid AthleteId { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
}

public sealed class CreatePaymentOrderRequest
{
    public Guid WorkoutPlanId { get; set; }
}

public sealed class ProcessPaymentRequest
{
    public Guid PaymentOrderId { get; set; }
    public string GatewayRef { get; set; } = string.Empty;
    public bool SimulateSuccess { get; set; } = true;
}

public sealed class PaymentResultDto
{
    public Guid PaymentOrderId { get; set; }
    public PaymentStatus Status { get; set; }
    public Guid? TransactionId { get; set; }
    public ProgramStatus? PlanStatus { get; set; }
}
