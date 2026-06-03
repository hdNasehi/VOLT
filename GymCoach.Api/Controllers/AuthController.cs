using GymCoach.Api.Services;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymCoach.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IRegistrationService registrationService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<Result<RegisterUserResultDto>>> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await registrationService.RegisterAndLinkAsync(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
