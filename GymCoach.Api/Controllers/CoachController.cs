using GymCoach.Api.Services;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymCoach.Api.Controllers;

[ApiController]
[Route("api/coach")]
public class CoachController(
    ICoachDashboardService dashboardService,
    ICurrentCoachProvider coachProvider) : ControllerBase
{
    [HttpGet("dashboard")]
    public async Task<ActionResult<Result<CoachDashboardDto>>> GetDashboard(CancellationToken cancellationToken)
    {
        var result = await dashboardService.GetDashboardAsync(coachProvider.GetCoachId(), cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
