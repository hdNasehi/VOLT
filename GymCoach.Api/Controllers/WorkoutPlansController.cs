using GymCoach.Api.Services;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCoach.Api.Controllers;

[ApiController]
[Route("api/workout-plans")]
[Authorize]
public class WorkoutPlansController(IWorkoutPlanService planService, ICurrentCoachProvider coachProvider) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Result<PagedResult<WorkoutPlanDetailDto>>>> List(
        [FromQuery] PagedRequest request,
        [FromQuery] Guid? athleteId,
        CancellationToken cancellationToken)
    {
        var coachId = coachProvider.TryGetCoachId();
        var result = await planService.ListAsync(request, coachId, athleteId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Result<WorkoutPlanDetailDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await planService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.CoachOnly)]
    public async Task<ActionResult<Result<WorkoutPlanDetailDto>>> Create(
        [FromBody] CreateWorkoutPlanRequest request,
        CancellationToken cancellationToken)
    {
        var result = await planService.CreateAsync(coachProvider.GetCoachId(), request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = Permissions.CoachOnly)]
    public async Task<ActionResult<Result<WorkoutPlanDetailDto>>> Update(
        Guid id,
        [FromBody] UpdateWorkoutPlanRequest request,
        CancellationToken cancellationToken)
    {
        var result = await planService.UpdateAsync(coachProvider.GetCoachId(), id, request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{id:guid}/publish")]
    [Authorize(Policy = Permissions.CoachOnly)]
    public async Task<ActionResult<Result<WorkoutPlanDetailDto>>> Publish(Guid id, CancellationToken cancellationToken)
    {
        var result = await planService.PublishAsync(coachProvider.GetCoachId(), id, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
