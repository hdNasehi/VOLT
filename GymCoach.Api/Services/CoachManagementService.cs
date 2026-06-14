using GymCoach.Api.Mapping;
using GymCoach.Database;
using GymCoach.Database.Entities;
using GymCoach.Database.Repositories;
using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Api.Services;

public interface ICoachManagementService
{
    Task<Result<PagedResult<CoachDetailDto>>> ListAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<Result<CoachDetailDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<CoachDetailDto>> UpdateApprovalAsync(Guid id, UpdateCoachApprovalRequest request, CancellationToken cancellationToken = default);
    Task<Result<PagedResult<AthleteCoachRequestDto>>> ListRequestsAsync(Guid coachId, PagedRequest request, CancellationToken cancellationToken = default);
    Task<Result<AthleteCoachRequestDto>> AcceptRequestAsync(Guid coachId, Guid requestId, CancellationToken cancellationToken = default);
    Task<Result<AthleteCoachRequestDto>> RejectRequestAsync(Guid coachId, Guid requestId, RejectRequestDto request, CancellationToken cancellationToken = default);
}

public sealed class CoachManagementService(
    ICoachRepository coaches,
    IAthleteRepository athletes,
    GymCoachDbContext db) : ICoachManagementService
{
    public async Task<Result<PagedResult<CoachDetailDto>>> ListAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        CoachApprovalStatus? status = null;
        if (!string.IsNullOrWhiteSpace(request.Status) &&
            Enum.TryParse<CoachApprovalStatus>(request.Status, true, out var parsed))
        {
            status = parsed;
        }

        var (items, total) = await coaches.ListAsync(status, request.Page, request.PageSize, cancellationToken);
        return Result<PagedResult<CoachDetailDto>>.Success(new PagedResult<CoachDetailDto>
        {
            Items = items.Select(EntityMappers.ToDto).ToList(),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        });
    }

    public async Task<Result<CoachDetailDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var coach = await coaches.GetByIdAsync(id, cancellationToken);
        return coach is null
            ? Result<CoachDetailDto>.Failure("Coach not found.")
            : Result<CoachDetailDto>.Success(EntityMappers.ToDto(coach));
    }

    public async Task<Result<CoachDetailDto>> UpdateApprovalAsync(
        Guid id, UpdateCoachApprovalRequest request, CancellationToken cancellationToken = default)
    {
        var coach = await coaches.GetByIdAsync(id, cancellationToken);
        if (coach is null)
        {
            return Result<CoachDetailDto>.Failure("Coach not found.");
        }

        coach.ApprovalStatus = request.Status;
        await coaches.SaveChangesAsync(cancellationToken);
        return Result<CoachDetailDto>.Success(EntityMappers.ToDto(coach));
    }

    public async Task<Result<PagedResult<AthleteCoachRequestDto>>> ListRequestsAsync(
        Guid coachId, PagedRequest request, CancellationToken cancellationToken = default)
    {
        RequestStatus? status = null;
        if (!string.IsNullOrWhiteSpace(request.Status) &&
            Enum.TryParse<RequestStatus>(request.Status, true, out var parsed))
        {
            status = parsed;
        }

        var (items, total) = await athletes.ListRequestsForCoachAsync(coachId, status, request.Page, request.PageSize, cancellationToken);
        return Result<PagedResult<AthleteCoachRequestDto>>.Success(new PagedResult<AthleteCoachRequestDto>
        {
            Items = items.Select(EntityMappers.ToDto).ToList(),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        });
    }

    public async Task<Result<AthleteCoachRequestDto>> AcceptRequestAsync(
        Guid coachId, Guid requestId, CancellationToken cancellationToken = default)
    {
        var athleteRequest = await athletes.GetRequestAsync(requestId, cancellationToken);
        if (athleteRequest is null || athleteRequest.CoachId != coachId)
        {
            return Result<AthleteCoachRequestDto>.Failure("Request not found.");
        }

        if (athleteRequest.Status != RequestStatus.Pending)
        {
            return Result<AthleteCoachRequestDto>.Failure("Request is not pending.");
        }

        athleteRequest.Status = RequestStatus.Accepted;
        athleteRequest.RespondedAt = DateTime.UtcNow;

        var existingLink = await db.CoachAthletes.FirstOrDefaultAsync(ca =>
            ca.CoachId == coachId && ca.AthleteId == athleteRequest.AthleteId && ca.Role == CoachAthleteRole.Fitness,
            cancellationToken);

        if (existingLink is null)
        {
            db.CoachAthletes.Add(new CoachAthlete
            {
                Id = Guid.NewGuid(),
                CoachId = coachId,
                AthleteId = athleteRequest.AthleteId,
                Role = CoachAthleteRole.Fitness,
                IsActive = true,
                AssignedAt = DateTime.UtcNow
            });
        }
        else
        {
            existingLink.IsActive = true;
            existingLink.AssignedAt = DateTime.UtcNow;
        }

        await athletes.SaveChangesAsync(cancellationToken);
        return Result<AthleteCoachRequestDto>.Success(EntityMappers.ToDto(athleteRequest));
    }

    public async Task<Result<AthleteCoachRequestDto>> RejectRequestAsync(
        Guid coachId, Guid requestId, RejectRequestDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            return Result<AthleteCoachRequestDto>.Failure("Rejection reason is required.");
        }

        var athleteRequest = await athletes.GetRequestAsync(requestId, cancellationToken);
        if (athleteRequest is null || athleteRequest.CoachId != coachId)
        {
            return Result<AthleteCoachRequestDto>.Failure("Request not found.");
        }

        if (athleteRequest.Status != RequestStatus.Pending)
        {
            return Result<AthleteCoachRequestDto>.Failure("Request is not pending.");
        }

        athleteRequest.Status = RequestStatus.Rejected;
        athleteRequest.RejectionReason = request.Reason.Trim();
        athleteRequest.RespondedAt = DateTime.UtcNow;
        await athletes.SaveChangesAsync(cancellationToken);
        return Result<AthleteCoachRequestDto>.Success(EntityMappers.ToDto(athleteRequest));
    }
}
