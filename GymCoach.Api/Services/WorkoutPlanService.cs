using GymCoach.Api.Mapping;
using GymCoach.Database.Entities;
using GymCoach.Database.Repositories;
using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;

namespace GymCoach.Api.Services;

public interface IWorkoutPlanService
{
    Task<Result<PagedResult<WorkoutPlanDetailDto>>> ListAsync(PagedRequest request, Guid? coachId, Guid? athleteId, CancellationToken cancellationToken = default);
    Task<Result<WorkoutPlanDetailDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<WorkoutPlanDetailDto>> CreateAsync(Guid coachId, CreateWorkoutPlanRequest request, CancellationToken cancellationToken = default);
    Task<Result<WorkoutPlanDetailDto>> UpdateAsync(Guid coachId, Guid id, UpdateWorkoutPlanRequest request, CancellationToken cancellationToken = default);
    Task<Result<WorkoutPlanDetailDto>> PublishAsync(Guid coachId, Guid id, CancellationToken cancellationToken = default);
}

public sealed class WorkoutPlanService(
    IWorkoutPlanRepository plans,
    IAthleteRepository athletes) : IWorkoutPlanService
{
    public async Task<Result<PagedResult<WorkoutPlanDetailDto>>> ListAsync(
        PagedRequest request, Guid? coachId, Guid? athleteId, CancellationToken cancellationToken = default)
    {
        var (items, total) = await plans.ListAsync(coachId, athleteId, request.Status, request.Page, request.PageSize, cancellationToken);
        return Result<PagedResult<WorkoutPlanDetailDto>>.Success(new PagedResult<WorkoutPlanDetailDto>
        {
            Items = items.Select(EntityMappers.ToDetailDto).ToList(),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        });
    }

    public async Task<Result<WorkoutPlanDetailDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var plan = await plans.GetDetailAsync(id, cancellationToken);
        return plan is null
            ? Result<WorkoutPlanDetailDto>.Failure("Workout plan not found.")
            : Result<WorkoutPlanDetailDto>.Success(EntityMappers.ToDetailDto(plan));
    }

    public async Task<Result<WorkoutPlanDetailDto>> CreateAsync(
        Guid coachId, CreateWorkoutPlanRequest request, CancellationToken cancellationToken = default)
    {
        if (!await athletes.IsLinkedToCoachAsync(coachId, request.AthleteId, cancellationToken))
        {
            return Result<WorkoutPlanDetailDto>.Failure("Athlete is not linked to this coach.");
        }

        var endDate = request.StartDate.AddDays(request.DurationWeeks * 7);
        var plan = new WorkoutPlan
        {
            Id = Guid.NewGuid(),
            CoachId = coachId,
            AthleteId = request.AthleteId,
            NameEn = request.NameEn,
            NameFa = request.NameFa,
            DescriptionEn = request.DescriptionEn,
            DescriptionFa = request.DescriptionFa,
            Goal = request.Goal,
            DurationWeeks = request.DurationWeeks,
            DaysPerWeek = request.DaysPerWeek,
            Price = request.Price,
            StartDate = request.StartDate,
            EndDate = endDate,
            Status = ProgramStatus.Draft
        };

        await plans.AddAsync(plan, cancellationToken);
        await plans.SaveChangesAsync(cancellationToken);
        return Result<WorkoutPlanDetailDto>.Success(EntityMappers.ToDetailDto(plan));
    }

    public async Task<Result<WorkoutPlanDetailDto>> UpdateAsync(
        Guid coachId, Guid id, UpdateWorkoutPlanRequest request, CancellationToken cancellationToken = default)
    {
        var plan = await plans.GetByIdAsync(id, cancellationToken);
        if (plan is null || plan.CoachId != coachId)
        {
            return Result<WorkoutPlanDetailDto>.Failure("Workout plan not found.");
        }

        if (plan.Status is ProgramStatus.Active or ProgramStatus.Completed)
        {
            return Result<WorkoutPlanDetailDto>.Failure("Active or completed plans cannot be edited.");
        }

        plan.NameEn = request.NameEn;
        plan.NameFa = request.NameFa;
        plan.DescriptionEn = request.DescriptionEn;
        plan.DescriptionFa = request.DescriptionFa;
        plan.Goal = request.Goal;
        plan.DurationWeeks = request.DurationWeeks;
        plan.DaysPerWeek = request.DaysPerWeek;
        plan.Price = request.Price;
        plan.StartDate = request.StartDate;
        plan.EndDate = request.EndDate ?? request.StartDate.AddDays(request.DurationWeeks * 7);
        await plans.SaveChangesAsync(cancellationToken);
        return Result<WorkoutPlanDetailDto>.Success(EntityMappers.ToDetailDto(plan));
    }

    public async Task<Result<WorkoutPlanDetailDto>> PublishAsync(Guid coachId, Guid id, CancellationToken cancellationToken = default)
    {
        var plan = await plans.GetByIdAsync(id, cancellationToken);
        if (plan is null || plan.CoachId != coachId)
        {
            return Result<WorkoutPlanDetailDto>.Failure("Workout plan not found.");
        }

        if (plan.Status != ProgramStatus.Draft)
        {
            return Result<WorkoutPlanDetailDto>.Failure("Only draft plans can be published.");
        }

        plan.Status = plan.Price > 0 ? ProgramStatus.PendingPayment : ProgramStatus.Active;
        await plans.SaveChangesAsync(cancellationToken);
        return Result<WorkoutPlanDetailDto>.Success(EntityMappers.ToDetailDto(plan));
    }
}
