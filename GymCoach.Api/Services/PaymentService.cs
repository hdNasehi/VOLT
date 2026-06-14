using GymCoach.Database.Entities;
using GymCoach.Database.Repositories;
using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;

namespace GymCoach.Api.Services;

public interface IPaymentService
{
    Task<Result<PaymentOrderDto>> CreateOrderAsync(Guid athleteId, CreatePaymentOrderRequest request, CancellationToken cancellationToken = default);
    Task<Result<PaymentResultDto>> ProcessPaymentAsync(ProcessPaymentRequest request, CancellationToken cancellationToken = default);
}

public sealed class PaymentService(
    IPaymentRepository payments,
    IWorkoutPlanRepository plans) : IPaymentService
{
    public async Task<Result<PaymentOrderDto>> CreateOrderAsync(
        Guid athleteId, CreatePaymentOrderRequest request, CancellationToken cancellationToken = default)
    {
        var plan = await plans.GetByIdAsync(request.WorkoutPlanId, cancellationToken);
        if (plan is null || plan.AthleteId != athleteId)
        {
            return Result<PaymentOrderDto>.Failure("Workout plan not found.");
        }

        if (plan.Status != ProgramStatus.PendingPayment)
        {
            return Result<PaymentOrderDto>.Failure("Plan is not awaiting payment.");
        }

        var order = new PaymentOrder
        {
            Id = Guid.NewGuid(),
            AthleteId = athleteId,
            WorkoutPlanId = plan.Id,
            Amount = plan.Price,
            Status = PaymentStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await payments.AddOrderAsync(order, cancellationToken);
        await payments.SaveChangesAsync(cancellationToken);

        return Result<PaymentOrderDto>.Success(new PaymentOrderDto
        {
            Id = order.Id,
            AthleteId = order.AthleteId,
            WorkoutPlanId = order.WorkoutPlanId,
            Amount = order.Amount,
            Status = order.Status,
            CreatedAt = order.CreatedAt
        });
    }

    public async Task<Result<PaymentResultDto>> ProcessPaymentAsync(
        ProcessPaymentRequest request, CancellationToken cancellationToken = default)
    {
        var order = await payments.GetOrderAsync(request.PaymentOrderId, cancellationToken);
        if (order is null)
        {
            return Result<PaymentResultDto>.Failure("Payment order not found.");
        }

        if (order.Status != PaymentStatus.Pending)
        {
            return Result<PaymentResultDto>.Failure("Order is not pending.");
        }

        if (!request.SimulateSuccess)
        {
            order.Status = PaymentStatus.Failed;
            await payments.AddTransactionAsync(new PaymentTransaction
            {
                Id = Guid.NewGuid(),
                PaymentOrderId = order.Id,
                GatewayRef = request.GatewayRef,
                Amount = order.Amount,
                Status = PaymentStatus.Failed,
                CreatedAt = DateTime.UtcNow
            }, cancellationToken);
            await payments.SaveChangesAsync(cancellationToken);
            return Result<PaymentResultDto>.Success(new PaymentResultDto
            {
                PaymentOrderId = order.Id,
                Status = PaymentStatus.Failed
            });
        }

        order.Status = PaymentStatus.Paid;
        order.PaidAt = DateTime.UtcNow;
        order.WorkoutPlan.Status = ProgramStatus.Active;

        var transaction = new PaymentTransaction
        {
            Id = Guid.NewGuid(),
            PaymentOrderId = order.Id,
            GatewayRef = request.GatewayRef,
            Amount = order.Amount,
            Status = PaymentStatus.Paid,
            CreatedAt = DateTime.UtcNow
        };
        await payments.AddTransactionAsync(transaction, cancellationToken);

        var gym = await payments.GetPrimaryGymForCoachAsync(order.WorkoutPlan.CoachId, cancellationToken);
        var commissionRate = gym?.CommissionRate ?? 0m;
        var gymAmount = Math.Round(order.Amount * commissionRate, 2);
        var coachAmount = order.Amount - gymAmount;

        await payments.AddEarningAsync(new CoachEarning
        {
            Id = Guid.NewGuid(),
            CoachId = order.WorkoutPlan.CoachId,
            PaymentOrderId = order.Id,
            Amount = coachAmount,
            CreatedAt = DateTime.UtcNow
        }, cancellationToken);

        if (gym is not null && gymAmount > 0)
        {
            await payments.AddCommissionAsync(new GymCommission
            {
                Id = Guid.NewGuid(),
                GymId = gym.Id,
                PaymentOrderId = order.Id,
                Amount = gymAmount,
                CreatedAt = DateTime.UtcNow
            }, cancellationToken);
        }

        await payments.SaveChangesAsync(cancellationToken);

        return Result<PaymentResultDto>.Success(new PaymentResultDto
        {
            PaymentOrderId = order.Id,
            Status = PaymentStatus.Paid,
            TransactionId = transaction.Id,
            PlanStatus = ProgramStatus.Active
        });
    }
}
