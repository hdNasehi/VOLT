using GymCoach.Api.Services;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCoach.Api.Controllers;

[ApiController]
[Route("api/athletes")]
[Authorize]
public class AthletesController(
    IAthletePhoneService athletePhoneService,
    IAthleteRosterService athleteRosterService,
    IAthleteRequestService athleteRequestService,
    ICurrentCoachProvider coachProvider,
    ICurrentUserContext userContext) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<AthleteDto>>> GetAll(
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var athletes = await athleteRosterService.GetForCoachAsync(
            coachProvider.GetCoachId(),
            status,
            cancellationToken);
        return Ok(athletes);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<AthleteDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var athlete = await athleteRosterService.GetByIdForCoachAsync(
            coachProvider.GetCoachId(),
            id,
            cancellationToken);
        return athlete is null ? NotFound() : Ok(athlete);
    }

    [HttpPost("requests")]
    [Authorize(Policy = Permissions.AthleteOnly)]
    public async Task<ActionResult<Result<AthleteCoachRequestDto>>> SubmitRequest(
        [FromBody] CreateAthleteCoachRequest request,
        CancellationToken cancellationToken)
    {
        var athleteId = userContext.GetAthleteId();
        if (!athleteId.HasValue)
        {
            return BadRequest(Result<AthleteCoachRequestDto>.Failure("Athlete profile not found."));
        }

        var result = await athleteRequestService.SubmitRequestAsync(athleteId.Value, request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("requests/mine")]
    [Authorize(Policy = Permissions.AthleteOnly)]
    public async Task<ActionResult<Result<PagedResult<AthleteCoachRequestDto>>>> MyRequests(
        [FromQuery] PagedRequest request,
        CancellationToken cancellationToken)
    {
        var athleteId = userContext.GetAthleteId();
        if (!athleteId.HasValue)
        {
            return BadRequest(Result<PagedResult<AthleteCoachRequestDto>>.Failure("Athlete profile not found."));
        }

        var result = await athleteRequestService.ListMyRequestsAsync(athleteId.Value, request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("check-phone")]
    [Authorize(Policy = Permissions.CoachOnly)]
    public async Task<ActionResult<Result<CheckPhoneResultDto>>> CheckPhone(
        [FromBody] CheckPhoneRequest request,
        CancellationToken cancellationToken)
    {
        var result = await athletePhoneService.CheckPhoneAsync(coachProvider.GetCoachId(), request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("add-by-phone")]
    [Authorize(Policy = Permissions.CoachOnly)]
    public async Task<ActionResult<Result<AddAthleteByPhoneResultDto>>> AddByPhone(
        [FromBody] AddAthleteByPhoneRequest request,
        CancellationToken cancellationToken)
    {
        var result = await athletePhoneService.AddByPhoneAsync(coachProvider.GetCoachId(), request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
