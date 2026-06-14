using GymCoach.Api.Mapping;
using GymCoach.Database.Entities;
using GymCoach.Database.Repositories;
using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;

namespace GymCoach.Api.Services;

public interface IAthleteRequestService
{
    Task<Result<AthleteCoachRequestDto>> SubmitRequestAsync(Guid athleteId, CreateAthleteCoachRequest request, CancellationToken cancellationToken = default);
    Task<Result<PagedResult<AthleteCoachRequestDto>>> ListMyRequestsAsync(Guid athleteId, PagedRequest paging, CancellationToken cancellationToken = default);
}

public sealed class AthleteRequestService(IAthleteRepository athletes, ICoachRepository coaches) : IAthleteRequestService
{
    public async Task<Result<AthleteCoachRequestDto>> SubmitRequestAsync(
        Guid athleteId, CreateAthleteCoachRequest request, CancellationToken cancellationToken = default)
    {
        var coach = await coaches.GetByIdAsync(request.CoachId, cancellationToken);
        if (coach is null)
        {
            return Result<AthleteCoachRequestDto>.Failure("Coach not found.");
        }

        if (coach.ApprovalStatus != CoachApprovalStatus.Approved)
        {
            return Result<AthleteCoachRequestDto>.Failure("Coach is not approved.");
        }

        var entity = new AthleteCoachRequest
        {
            Id = Guid.NewGuid(),
            AthleteId = athleteId,
            CoachId = request.CoachId,
            Goal = request.Goal,
            Experience = request.Experience,
            HeightCm = request.HeightCm,
            WeightKg = request.WeightKg,
            Measurements = request.Measurements,
            Diseases = request.Diseases,
            Medications = request.Medications,
            Supplements = request.Supplements,
            Notes = request.Notes,
            Status = RequestStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await athletes.AddRequestAsync(entity, cancellationToken);
        await athletes.SaveChangesAsync(cancellationToken);

        entity.Athlete = (await athletes.GetByIdAsync(athleteId, cancellationToken))!;
        entity.Coach = coach;
        return Result<AthleteCoachRequestDto>.Success(EntityMappers.ToDto(entity));
    }

    public async Task<Result<PagedResult<AthleteCoachRequestDto>>> ListMyRequestsAsync(
        Guid athleteId, PagedRequest paging, CancellationToken cancellationToken = default)
    {
        RequestStatus? status = null;
        if (!string.IsNullOrWhiteSpace(paging.Status) &&
            Enum.TryParse<RequestStatus>(paging.Status, true, out var parsed))
        {
            status = parsed;
        }

        var (items, total) = await athletes.ListRequestsForAthleteAsync(
            athleteId, status, paging.Page, paging.PageSize, cancellationToken);

        return Result<PagedResult<AthleteCoachRequestDto>>.Success(new PagedResult<AthleteCoachRequestDto>
        {
            Items = items.Select(EntityMappers.ToDto).ToList(),
            Page = paging.Page,
            PageSize = paging.PageSize,
            TotalCount = total
        });
    }
}
