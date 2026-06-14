using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database.Repositories;

public interface IPaymentRepository
{
    Task<PaymentOrder?> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddOrderAsync(PaymentOrder order, CancellationToken cancellationToken = default);
    Task AddTransactionAsync(PaymentTransaction transaction, CancellationToken cancellationToken = default);
    Task AddEarningAsync(CoachEarning earning, CancellationToken cancellationToken = default);
    Task AddCommissionAsync(GymCommission commission, CancellationToken cancellationToken = default);
    Task<Gym?> GetPrimaryGymForCoachAsync(Guid coachId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public sealed class PaymentRepository(GymCoachDbContext db) : IPaymentRepository
{
    public Task<PaymentOrder?> GetOrderAsync(Guid id, CancellationToken cancellationToken = default) =>
        db.PaymentOrders
            .Include(o => o.WorkoutPlan)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public async Task AddOrderAsync(PaymentOrder order, CancellationToken cancellationToken = default) =>
        db.PaymentOrders.Add(order);

    public async Task AddTransactionAsync(PaymentTransaction transaction, CancellationToken cancellationToken = default) =>
        db.PaymentTransactions.Add(transaction);

    public async Task AddEarningAsync(CoachEarning earning, CancellationToken cancellationToken = default) =>
        db.CoachEarnings.Add(earning);

    public async Task AddCommissionAsync(GymCommission commission, CancellationToken cancellationToken = default) =>
        db.GymCommissions.Add(commission);

    public Task<Gym?> GetPrimaryGymForCoachAsync(Guid coachId, CancellationToken cancellationToken = default) =>
        db.GymCoaches
            .AsNoTracking()
            .Where(gc => gc.CoachId == coachId && gc.IsActive)
            .Select(gc => gc.Gym)
            .FirstOrDefaultAsync(cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        db.SaveChangesAsync(cancellationToken);
}
