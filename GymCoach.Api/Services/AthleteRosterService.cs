using GymCoach.Database;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Api.Services;

public interface IAthleteRosterService
{
    Task<IReadOnlyList<AthleteDto>> GetForCoachAsync(
        Guid coachId,
        string? status = null,
        CancellationToken cancellationToken = default);

    Task<AthleteDto?> GetByIdForCoachAsync(
        Guid coachId,
        Guid athleteId,
        CancellationToken cancellationToken = default);
}

public sealed class AthleteRosterService(GymCoachDbContext db) : IAthleteRosterService
{
    public async Task<IReadOnlyList<AthleteDto>> GetForCoachAsync(
        Guid coachId,
        string? status = null,
        CancellationToken cancellationToken = default)
    {
        var athleteIds = await db.CoachAthletes
            .AsNoTracking()
            .Where(ca => ca.CoachId == coachId && ca.IsActive && ca.Role == CoachAthleteRole.Fitness)
            .Select(ca => ca.AthleteId)
            .ToListAsync(cancellationToken);

        if (athleteIds.Count == 0)
        {
            return [];
        }

        var query = db.Athletes.AsNoTracking().Where(a => athleteIds.Contains(a.Id));

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = status.Trim() switch
            {
                nameof(AthleteStatus.Active) => query.Where(a => a.Status == AthleteStatus.Active),
                nameof(AthleteStatus.Inactive) => query.Where(a =>
                    a.Status == AthleteStatus.Inactive || a.Status == AthleteStatus.OnHold),
                _ => Enum.TryParse<AthleteStatus>(status, true, out var parsed)
                    ? query.Where(a => a.Status == parsed)
                    : query
            };
        }

        var athletes = await query
            .OrderBy(a => a.LastName)
            .ThenBy(a => a.FirstName)
            .ToListAsync(cancellationToken);

        return athletes.Select(MapToDto).ToList();
    }

    public async Task<AthleteDto?> GetByIdForCoachAsync(
        Guid coachId,
        Guid athleteId,
        CancellationToken cancellationToken = default)
    {
        var isLinked = await db.CoachAthletes
            .AsNoTracking()
            .AnyAsync(ca => ca.CoachId == coachId
                && ca.AthleteId == athleteId
                && ca.IsActive
                && ca.Role == CoachAthleteRole.Fitness, cancellationToken);

        if (!isLinked)
        {
            return null;
        }

        var athlete = await db.Athletes
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == athleteId, cancellationToken);

        return athlete is null ? null : MapToDto(athlete);
    }

    private static AthleteDto MapToDto(Database.Entities.Athlete athlete)
    {
        var fullName = $"{athlete.FirstName} {athlete.LastName}".Trim();
        return new AthleteDto
        {
            Id = athlete.Id,
            FullName = fullName,
            FullNameFa = fullName,
            Email = athlete.Email,
            PhoneNumber = athlete.PhoneNumber,
            AvatarUrl = athlete.ProfilePhotoUrl,
            DateOfBirth = athlete.BirthDate,
            Status = athlete.Status,
            Goal = athlete.Goal,
            WeightKg = athlete.WeightKg
        };
    }
}
