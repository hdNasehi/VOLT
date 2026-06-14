using GymCoach.Api.Services;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCoach.Api.Controllers;

[ApiController]
[Route("api/payments")]
[Authorize]
public class PaymentsController(IPaymentService paymentService, ICurrentUserContext userContext) : ControllerBase
{
    [HttpPost("orders")]
    [Authorize(Policy = Permissions.AthleteOnly)]
    public async Task<ActionResult<Result<PaymentOrderDto>>> CreateOrder(
        [FromBody] CreatePaymentOrderRequest request,
        CancellationToken cancellationToken)
    {
        var athleteId = userContext.GetAthleteId();
        if (!athleteId.HasValue)
        {
            return BadRequest(Result<PaymentOrderDto>.Failure("Athlete profile not found."));
        }

        var result = await paymentService.CreateOrderAsync(athleteId.Value, request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("process")]
    [Authorize(Policy = Permissions.AthleteOnly)]
    public async Task<ActionResult<Result<PaymentResultDto>>> Process(
        [FromBody] ProcessPaymentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await paymentService.ProcessPaymentAsync(request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
