using GymCoach.Api.Services;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymCoach.Api.Controllers;

[ApiController]
[Route("api/athletes")]
public class AthletesController(
    IAthletePhoneService athletePhoneService,
    ICurrentCoachProvider coachProvider) : ControllerBase
{
    [HttpPost("check-phone")]
    public async Task<ActionResult<Result<CheckPhoneResultDto>>> CheckPhone(
        [FromBody] CheckPhoneRequest request,
        CancellationToken cancellationToken)
    {
        var result = await athletePhoneService.CheckPhoneAsync(coachProvider.GetCoachId(), request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("add-by-phone")]
    public async Task<ActionResult<Result<AddAthleteByPhoneResultDto>>> AddByPhone(
        [FromBody] AddAthleteByPhoneRequest request,
        CancellationToken cancellationToken)
    {
        var result = await athletePhoneService.AddByPhoneAsync(coachProvider.GetCoachId(), request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
