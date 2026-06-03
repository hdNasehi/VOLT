using GymCoach.Database;
using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Api.Services;

public interface ICoachDashboardService
{
    Task<Result<CoachDashboardDto>> GetDashboardAsync(Guid coachId, CancellationToken cancellationToken = default);
}

public sealed class CoachDashboardService(GymCoachDbContext db) : ICoachDashboardService
{
    private const int NoWorkoutThresholdDays = 7;
    private const int ProgramExpiringThresholdDays = 5;

    public async Task<Result<CoachDashboardDto>> GetDashboardAsync(
        Guid coachId,
        CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var noWorkoutCutoff = today.AddDays(-NoWorkoutThresholdDays);
        var expiringCutoff = today.AddDays(ProgramExpiringThresholdDays);

        var athleteIds = await db.CoachAthletes
            .AsNoTracking()
            .Where(ca => ca.CoachId == coachId && ca.IsActive && ca.Role == CoachAthleteRole.Fitness)
            .Select(ca => ca.AthleteId)
            .ToListAsync(cancellationToken);

        if (athleteIds.Count == 0)
        {
            return Result<CoachDashboardDto>.Success(new CoachDashboardDto());
        }

        var athletes = await db.Athletes
            .AsNoTracking()
            .Where(a => athleteIds.Contains(a.Id))
            .ToListAsync(cancellationToken);

        var activeCount = athletes.Count(a => a.Status == AthleteStatus.Active);
        var inactiveOrOnHold = athletes.Count(a => a.Status is AthleteStatus.Inactive or AthleteStatus.OnHold);

        var lastWorkouts = await db.WorkoutSessions
            .AsNoTracking()
            .Where(ws => athleteIds.Contains(ws.AthleteId) && ws.IsCompleted)
            .GroupBy(ws => ws.AthleteId)
            .Select(g => new { AthleteId = g.Key, LastDate = g.Max(x => DateOnly.FromDateTime(x.SessionDate)) })
            .ToDictionaryAsync(x => x.AthleteId, x => x.LastDate, cancellationToken);

        var activePrograms = await db.WorkoutPlans
            .AsNoTracking()
            .Where(p => athleteIds.Contains(p.AthleteId) && p.Status == ProgramStatus.Active)
            .ToListAsync(cancellationToken);

        var athletesWithActiveProgram = activePrograms.Select(p => p.AthleteId).ToHashSet();
        var needsNewProgramIds = athleteIds.Where(id => !athletesWithActiveProgram.Contains(id)).ToHashSet();

        var noWorkoutIds = athleteIds
            .Where(id =>
            {
                if (!lastWorkouts.TryGetValue(id, out var last))
                {
                    return true;
                }

                return last <= noWorkoutCutoff;
            })
            .ToHashSet();

        var attentionItems = new List<CoachAlertDto>();

        foreach (var athlete in athletes)
        {
            if (noWorkoutIds.Contains(athlete.Id))
            {
                lastWorkouts.TryGetValue(athlete.Id, out var lastWorkout);
                attentionItems.Add(new CoachAlertDto
                {
                    AthleteId = athlete.Id,
                    AthleteNameEn = $"{athlete.FirstName} {athlete.LastName}".Trim(),
                    AthleteNameFa = $"{athlete.FirstName} {athlete.LastName}".Trim(),
                    ProfilePhotoUrl = athlete.ProfilePhotoUrl,
                    AlertType = CoachAlertType.NoWorkout7Days,
                    Severity = CoachAlertSeverity.Severe,
                    MessageEn = lastWorkout == default
                        ? "No workouts logged yet"
                        : $"No workout for {(today.DayNumber - lastWorkout.DayNumber)} days",
                    MessageFa = lastWorkout == default
                        ? "هنوز تمرینی ثبت نشده"
                        : $"بدون تمرین به مدت {(today.DayNumber - lastWorkout.DayNumber)} روز",
                    RelatedDate = lastWorkout == default ? null : lastWorkout
                });
            }

            if (needsNewProgramIds.Contains(athlete.Id))
            {
                attentionItems.Add(new CoachAlertDto
                {
                    AthleteId = athlete.Id,
                    AthleteNameEn = $"{athlete.FirstName} {athlete.LastName}".Trim(),
                    AthleteNameFa = $"{athlete.FirstName} {athlete.LastName}".Trim(),
                    ProfilePhotoUrl = athlete.ProfilePhotoUrl,
                    AlertType = CoachAlertType.NeedsNewProgram,
                    Severity = CoachAlertSeverity.Warning,
                    MessageEn = "Needs a new program",
                    MessageFa = "نیاز به برنامه جدید"
                });
            }
        }

        var expiringPrograms = activePrograms
            .Where(p => p.EndDate.HasValue && p.EndDate.Value <= expiringCutoff)
            .OrderBy(p => p.EndDate)
            .ToList();

        foreach (var program in expiringPrograms)
        {
            var athlete = athletes.First(a => a.Id == program.AthleteId);
            var endDate = program.EndDate!.Value;
            var daysRemaining = endDate.DayNumber - today.DayNumber;
            var isExpired = daysRemaining < 0;

            attentionItems.Add(new CoachAlertDto
            {
                AthleteId = athlete.Id,
                AthleteNameEn = $"{athlete.FirstName} {athlete.LastName}".Trim(),
                AthleteNameFa = $"{athlete.FirstName} {athlete.LastName}".Trim(),
                ProfilePhotoUrl = athlete.ProfilePhotoUrl,
                AlertType = isExpired ? CoachAlertType.ProgramExpired : CoachAlertType.ProgramExpiring,
                Severity = isExpired ? CoachAlertSeverity.Severe : CoachAlertSeverity.Warning,
                MessageEn = isExpired
                    ? $"Program expired {Math.Abs(daysRemaining)} days ago"
                    : $"Program expires in {daysRemaining} days",
                MessageFa = isExpired
                    ? $"برنامه {Math.Abs(daysRemaining)} روز پیش منقضی شد"
                    : $"برنامه تا {daysRemaining} روز دیگر منقضی می‌شود",
                RelatedDate = endDate
            });
        }

        attentionItems = attentionItems
            .OrderByDescending(a => a.Severity)
            .ThenBy(a => a.RelatedDate)
            .ToList();

        var programsExpiring = expiringPrograms
            .Select(p =>
            {
                var athlete = athletes.First(a => a.Id == p.AthleteId);
                var endDate = p.EndDate!.Value;
                var daysRemaining = endDate.DayNumber - today.DayNumber;
                return new ProgramExpiringDto
                {
                    ProgramId = p.Id,
                    AthleteId = athlete.Id,
                    AthleteNameEn = $"{athlete.FirstName} {athlete.LastName}".Trim(),
                    AthleteNameFa = $"{athlete.FirstName} {athlete.LastName}".Trim(),
                    ProgramNameEn = p.NameEn,
                    ProgramNameFa = p.NameFa,
                    EndDate = endDate,
                    DaysRemaining = daysRemaining,
                    IsExpired = daysRemaining < 0
                };
            })
            .ToList();

        var recentMeasurements = await db.Measurements
            .AsNoTracking()
            .Where(m => athleteIds.Contains(m.AthleteId))
            .OrderByDescending(m => m.MeasurementDate)
            .Take(8)
            .Join(db.Athletes.AsNoTracking(), m => m.AthleteId, a => a.Id, (m, a) => new RecentMeasurementDto
            {
                Id = m.Id,
                AthleteId = m.AthleteId,
                AthleteNameEn = $"{a.FirstName} {a.LastName}".Trim(),
                AthleteNameFa = $"{a.FirstName} {a.LastName}".Trim(),
                Weight = m.Weight,
                MeasurementDate = m.MeasurementDate
            })
            .ToListAsync(cancellationToken);

        var recentPhotos = await db.ProgressPhotos
            .AsNoTracking()
            .Where(p => athleteIds.Contains(p.AthleteId))
            .OrderByDescending(p => p.Date)
            .Take(8)
            .Join(db.Athletes.AsNoTracking(), p => p.AthleteId, a => a.Id, (p, a) => new RecentProgressPhotoDto
            {
                Id = p.Id,
                AthleteId = p.AthleteId,
                AthleteNameEn = $"{a.FirstName} {a.LastName}".Trim(),
                AthleteNameFa = $"{a.FirstName} {a.LastName}".Trim(),
                Category = p.Category,
                PhotoUrl = p.PhotoUrl,
                Date = p.Date
            })
            .ToListAsync(cancellationToken);

        return Result<CoachDashboardDto>.Success(new CoachDashboardDto
        {
            TotalAthletes = athletes.Count,
            ActiveAthletes = activeCount,
            InactiveOrOnHoldAthletes = inactiveOrOnHold,
            NeedsNewProgramCount = needsNewProgramIds.Count,
            NoWorkout7DaysCount = noWorkoutIds.Count,
            AttentionItems = attentionItems,
            ProgramsExpiring = programsExpiring,
            RecentMeasurements = recentMeasurements,
            RecentProgressPhotos = recentPhotos
        });
    }
}
