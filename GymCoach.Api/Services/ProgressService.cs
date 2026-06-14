using GymCoach.Api.Mapping;
using GymCoach.Database.Entities;
using GymCoach.Database.Repositories;
using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;

namespace GymCoach.Api.Services;

public interface IProgressService
{
    Task<Result<PagedResult<MeasurementRecordDto>>> GetMeasurementsAsync(
        Guid athleteId, PagedRequest request, CancellationToken cancellationToken = default);
    Task<Result<MeasurementRecordDto>> AddMeasurementAsync(
        Guid athleteId, CreateMeasurementRequest request, CancellationToken cancellationToken = default);
    Task<Result<PagedResult<ProgressPhotoDto>>> GetPhotosAsync(
        Guid athleteId, PagedRequest request, CancellationToken cancellationToken = default);
    Task<Result<ProgressPhotoDto>> AddPhotoAsync(
        Guid athleteId, CreateProgressPhotoRequest request, CancellationToken cancellationToken = default);
    Task<Result<PagedResult<PersonalRecordDto>>> GetPersonalRecordsAsync(
        Guid athleteId, PagedRequest request, CancellationToken cancellationToken = default);
}

public sealed class ProgressService(IProgressRepository progress) : IProgressService
{
    public async Task<Result<PagedResult<MeasurementRecordDto>>> GetMeasurementsAsync(
        Guid athleteId, PagedRequest request, CancellationToken cancellationToken = default)
    {
        var (items, total) = await progress.ListMeasurementsAsync(athleteId, request.Page, request.PageSize, cancellationToken);
        return Result<PagedResult<MeasurementRecordDto>>.Success(new PagedResult<MeasurementRecordDto>
        {
            Items = items.Select(EntityMappers.ToDto).ToList(),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        });
    }

    public async Task<Result<MeasurementRecordDto>> AddMeasurementAsync(
        Guid athleteId, CreateMeasurementRequest request, CancellationToken cancellationToken = default)
    {
        var measurement = new Measurement
        {
            Id = Guid.NewGuid(),
            AthleteId = athleteId,
            Weight = request.Weight,
            Chest = request.Chest,
            Waist = request.Waist,
            Arms = request.Arms,
            Thighs = request.Thighs,
            Calves = request.Calves,
            Neck = request.Neck,
            BodyFatPercentage = request.BodyFatPercentage,
            MeasurementDate = request.MeasurementDate
        };
        await progress.AddMeasurementAsync(measurement, cancellationToken);
        await progress.SaveChangesAsync(cancellationToken);
        return Result<MeasurementRecordDto>.Success(EntityMappers.ToDto(measurement));
    }

    public async Task<Result<PagedResult<ProgressPhotoDto>>> GetPhotosAsync(
        Guid athleteId, PagedRequest request, CancellationToken cancellationToken = default)
    {
        var (items, total) = await progress.ListPhotosAsync(athleteId, request.Page, request.PageSize, cancellationToken);
        return Result<PagedResult<ProgressPhotoDto>>.Success(new PagedResult<ProgressPhotoDto>
        {
            Items = items.Select(EntityMappers.ToDto).ToList(),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        });
    }

    public async Task<Result<ProgressPhotoDto>> AddPhotoAsync(
        Guid athleteId, CreateProgressPhotoRequest request, CancellationToken cancellationToken = default)
    {
        var photo = new ProgressPhoto
        {
            Id = Guid.NewGuid(),
            AthleteId = athleteId,
            PhotoUrl = request.PhotoUrl,
            Date = request.DateTaken,
            Weight = request.Weight,
            Notes = request.Notes,
            Category = request.Category
        };
        await progress.AddPhotoAsync(photo, cancellationToken);
        await progress.SaveChangesAsync(cancellationToken);
        return Result<ProgressPhotoDto>.Success(EntityMappers.ToDto(photo));
    }

    public async Task<Result<PagedResult<PersonalRecordDto>>> GetPersonalRecordsAsync(
        Guid athleteId, PagedRequest request, CancellationToken cancellationToken = default)
    {
        var (items, total) = await progress.ListPersonalRecordsAsync(athleteId, request.Page, request.PageSize, cancellationToken);
        return Result<PagedResult<PersonalRecordDto>>.Success(new PagedResult<PersonalRecordDto>
        {
            Items = items.Select(EntityMappers.ToDto).ToList(),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        });
    }
}
