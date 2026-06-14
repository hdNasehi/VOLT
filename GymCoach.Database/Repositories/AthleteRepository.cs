using GymCoach.Database.Entities;
using GymCoach.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database.Repositories;

public interface IAthleteRepository
{
    Task<Athlete?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Athlete?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task AddAthleteAsync(Athlete athlete, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Athlete> Items, int Total)> ListForCoachAsync(
        Guid coachId, string? status, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<bool> IsLinkedToCoachAsync(Guid coachId, Guid athleteId, CancellationToken cancellationToken = default);
    Task AddRequestAsync(AthleteCoachRequest request, CancellationToken cancellationToken = default);
    Task<AthleteCoachRequest?> GetRequestAsync(Guid requestId, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<AthleteCoachRequest> Items, int Total)> ListRequestsForCoachAsync(
        Guid coachId, RequestStatus? status, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<AthleteCoachRequest> Items, int Total)> ListRequestsForAthleteAsync(
        Guid athleteId, RequestStatus? status, int page, int pageSize, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public sealed class AthleteRepository(GymCoachDbContext db) : IAthleteRepository
{
    public Task<Athlete?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        db.Athletes.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public Task<Athlete?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default) =>
        db.Athletes.FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);

    public Task AddAthleteAsync(Athlete athlete, CancellationToken cancellationToken = default)
    {
        db.Athletes.Add(athlete);
        return Task.CompletedTask;
    }

    public async Task<(IReadOnlyList<Athlete> Items, int Total)> ListForCoachAsync(
        Guid coachId, string? status, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var athleteIds = db.CoachAthletes
            .AsNoTracking()
            .Where(ca => ca.CoachId == coachId && ca.IsActive && ca.Role == CoachAthleteRole.Fitness)
            .Select(ca => ca.AthleteId);

        var query = db.Athletes.AsNoTracking().Where(a => athleteIds.Contains(a.Id));
        if (!string.IsNullOrWhiteSpace(status) &&
            Enum.TryParse<AthleteStatus>(status, true, out var parsed))
        {
            query = parsed == AthleteStatus.Inactive
                ? query.Where(a => a.Status == AthleteStatus.Inactive || a.Status == AthleteStatus.OnHold)
                : query.Where(a => a.Status == parsed);
        }

        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderBy(a => a.LastName).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public Task<bool> IsLinkedToCoachAsync(Guid coachId, Guid athleteId, CancellationToken cancellationToken = default) =>
        db.CoachAthletes.AnyAsync(ca =>
            ca.CoachId == coachId && ca.AthleteId == athleteId && ca.IsActive && ca.Role == CoachAthleteRole.Fitness,
            cancellationToken);

    public Task AddRequestAsync(AthleteCoachRequest request, CancellationToken cancellationToken = default)
    {
        db.AthleteCoachRequests.Add(request);
        return Task.CompletedTask;
    }

    public Task<AthleteCoachRequest?> GetRequestAsync(Guid requestId, CancellationToken cancellationToken = default) =>
        db.AthleteCoachRequests
            .Include(r => r.Athlete)
            .Include(r => r.Coach)
            .FirstOrDefaultAsync(r => r.Id == requestId, cancellationToken);

    public async Task<(IReadOnlyList<AthleteCoachRequest> Items, int Total)> ListRequestsForCoachAsync(
        Guid coachId, RequestStatus? status, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = db.AthleteCoachRequests.AsNoTracking()
            .Include(r => r.Athlete)
            .Include(r => r.Coach)
            .Where(r => r.CoachId == coachId);
        if (status.HasValue) query = query.Where(r => r.Status == status.Value);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(r => r.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task<(IReadOnlyList<AthleteCoachRequest> Items, int Total)> ListRequestsForAthleteAsync(
        Guid athleteId, RequestStatus? status, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = db.AthleteCoachRequests.AsNoTracking()
            .Include(r => r.Athlete)
            .Include(r => r.Coach)
            .Where(r => r.AthleteId == athleteId);
        if (status.HasValue) query = query.Where(r => r.Status == status.Value);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(r => r.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        db.SaveChangesAsync(cancellationToken);
}
