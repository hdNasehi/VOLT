using GymCoach.Database.Entities;
using GymCoach.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database.Repositories;

public interface ICoachRepository
{
    Task<Coach?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Coach?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Coach> Items, int Total)> ListAsync(
        CoachApprovalStatus? status, int page, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(Coach coach, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public sealed class CoachRepository(GymCoachDbContext db) : ICoachRepository
{
    public Task<Coach?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        db.Coaches.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public Task<Coach?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default) =>
        db.Coaches.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

    public async Task<(IReadOnlyList<Coach> Items, int Total)> ListAsync(
        CoachApprovalStatus? status, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = db.Coaches.AsNoTracking().AsQueryable();
        if (status.HasValue) query = query.Where(c => c.ApprovalStatus == status.Value);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderBy(c => c.LastName).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public Task AddAsync(Coach coach, CancellationToken cancellationToken = default)
    {
        db.Coaches.Add(coach);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        db.SaveChangesAsync(cancellationToken);
}
