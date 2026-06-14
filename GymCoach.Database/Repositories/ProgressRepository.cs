using GymCoach.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database.Repositories;

public interface IProgressRepository
{
    Task<(IReadOnlyList<Measurement> Items, int Total)> ListMeasurementsAsync(
        Guid athleteId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task AddMeasurementAsync(Measurement measurement, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<ProgressPhoto> Items, int Total)> ListPhotosAsync(
        Guid athleteId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task AddPhotoAsync(ProgressPhoto photo, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<PersonalRecord> Items, int Total)> ListPersonalRecordsAsync(
        Guid athleteId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public sealed class ProgressRepository(GymCoachDbContext db) : IProgressRepository
{
    public async Task<(IReadOnlyList<Measurement> Items, int Total)> ListMeasurementsAsync(
        Guid athleteId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = db.Measurements.AsNoTracking().Where(m => m.AthleteId == athleteId);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(m => m.MeasurementDate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public Task AddMeasurementAsync(Measurement measurement, CancellationToken cancellationToken = default)
    {
        db.Measurements.Add(measurement);
        return Task.CompletedTask;
    }

    public async Task<(IReadOnlyList<ProgressPhoto> Items, int Total)> ListPhotosAsync(
        Guid athleteId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = db.ProgressPhotos.AsNoTracking().Where(p => p.AthleteId == athleteId);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(p => p.Date).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public Task AddPhotoAsync(ProgressPhoto photo, CancellationToken cancellationToken = default)
    {
        db.ProgressPhotos.Add(photo);
        return Task.CompletedTask;
    }

    public async Task<(IReadOnlyList<PersonalRecord> Items, int Total)> ListPersonalRecordsAsync(
        Guid athleteId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = db.PersonalRecords.AsNoTracking()
            .Include(p => p.Exercise)
            .Where(p => p.AthleteId == athleteId);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(p => p.AchievedDate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        db.SaveChangesAsync(cancellationToken);
}
