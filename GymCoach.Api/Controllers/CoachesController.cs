using GymCoach.Api.Services;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCoach.Api.Controllers;

[ApiController]
[Route("api/coaches")]
[Authorize]
public class CoachesController(ICoachManagementService coachService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<Result<PagedResult<CoachDetailDto>>>> List(
        [FromQuery] PagedRequest request,
        CancellationToken cancellationToken)
    {
        var result = await coachService.ListAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<Result<CoachDetailDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await coachService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPut("{id:guid}/approval")]
    [Authorize(Policy = Permissions.ApproveCoaches)]
    public async Task<ActionResult<Result<CoachDetailDto>>> UpdateApproval(
        Guid id,
        [FromBody] UpdateCoachApprovalRequest request,
        CancellationToken cancellationToken)
    {
        var result = await coachService.UpdateApprovalAsync(id, request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("requests")]
    [Authorize(Policy = Permissions.CoachOnly)]
    public async Task<ActionResult<Result<PagedResult<AthleteCoachRequestDto>>>> ListRequests(
        [FromQuery] PagedRequest request,
        [FromServices] ICurrentCoachProvider coachProvider,
        CancellationToken cancellationToken)
    {
        var result = await coachService.ListRequestsAsync(coachProvider.GetCoachId(), request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("requests/{requestId:guid}/accept")]
    [Authorize(Policy = Permissions.CoachOnly)]
    public async Task<ActionResult<Result<AthleteCoachRequestDto>>> AcceptRequest(
        Guid requestId,
        [FromServices] ICurrentCoachProvider coachProvider,
        CancellationToken cancellationToken)
    {
        var result = await coachService.AcceptRequestAsync(coachProvider.GetCoachId(), requestId, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("requests/{requestId:guid}/reject")]
    [Authorize(Policy = Permissions.CoachOnly)]
    public async Task<ActionResult<Result<AthleteCoachRequestDto>>> RejectRequest(
        Guid requestId,
        [FromBody] RejectRequestDto request,
        [FromServices] ICurrentCoachProvider coachProvider,
        CancellationToken cancellationToken)
    {
        var result = await coachService.RejectRequestAsync(coachProvider.GetCoachId(), requestId, request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
