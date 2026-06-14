using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database.Repositories;

public interface IWorkoutPlanRepository
{
    Task<WorkoutPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkoutPlan?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<WorkoutPlan> Items, int Total)> ListAsync(
        Guid? coachId, Guid? athleteId, string? status, int page, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(WorkoutPlan plan, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public sealed class WorkoutPlanRepository(GymCoachDbContext db) : IWorkoutPlanRepository
{
    public Task<WorkoutPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        db.WorkoutPlans.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public Task<WorkoutPlan?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default) =>
        db.WorkoutPlans
            .AsNoTracking()
            .Include(p => p.Days.OrderBy(d => d.Order))
            .ThenInclude(d => d.Exercises.OrderBy(e => e.Order))
            .ThenInclude(e => e.Exercise)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<(IReadOnlyList<WorkoutPlan> Items, int Total)> ListAsync(
        Guid? coachId, Guid? athleteId, string? status, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = db.WorkoutPlans.AsNoTracking().AsQueryable();
        if (coachId.HasValue) query = query.Where(p => p.CoachId == coachId);
        if (athleteId.HasValue) query = query.Where(p => p.AthleteId == athleteId);
        if (!string.IsNullOrWhiteSpace(status) &&
            Enum.TryParse<GymCoach.Shared.Enums.ProgramStatus>(status, true, out var parsed))
        {
            query = query.Where(p => p.Status == parsed);
        }

        var total = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderByDescending(p => p.StartDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task AddAsync(WorkoutPlan plan, CancellationToken cancellationToken = default)
    {
        db.WorkoutPlans.Add(plan);
        await Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        db.SaveChangesAsync(cancellationToken);
}
