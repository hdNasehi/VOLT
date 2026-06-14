using GymCoach.Api.Services;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCoach.Api.Controllers;

[ApiController]
[Route("api/progress")]
[Authorize]
public class ProgressController(IProgressService progressService, ICurrentUserContext userContext) : ControllerBase
{
    [HttpGet("athletes/{athleteId:guid}/measurements")]
    public async Task<ActionResult<Result<PagedResult<MeasurementRecordDto>>>> GetMeasurements(
        Guid athleteId,
        [FromQuery] PagedRequest request,
        CancellationToken cancellationToken)
    {
        var result = await progressService.GetMeasurementsAsync(athleteId, request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("athletes/{athleteId:guid}/measurements")]
    public async Task<ActionResult<Result<MeasurementRecordDto>>> AddMeasurement(
        Guid athleteId,
        [FromBody] CreateMeasurementRequest request,
        CancellationToken cancellationToken)
    {
        var result = await progressService.AddMeasurementAsync(athleteId, request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("athletes/{athleteId:guid}/photos")]
    public async Task<ActionResult<Result<PagedResult<ProgressPhotoDto>>>> GetPhotos(
        Guid athleteId,
        [FromQuery] PagedRequest request,
        CancellationToken cancellationToken)
    {
        var result = await progressService.GetPhotosAsync(athleteId, request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("athletes/{athleteId:guid}/photos")]
    public async Task<ActionResult<Result<ProgressPhotoDto>>> AddPhoto(
        Guid athleteId,
        [FromBody] CreateProgressPhotoRequest request,
        CancellationToken cancellationToken)
    {
        var result = await progressService.AddPhotoAsync(athleteId, request, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("athletes/{athleteId:guid}/personal-records")]
    public async Task<ActionResult<Result<PagedResult<PersonalRecordDto>>>> GetPersonalRecords(
        Guid athleteId,
        [FromQuery] PagedRequest request,
        CancellationToken cancellationToken)
    {
        var result = await progressService.GetPersonalRecordsAsync(athleteId, request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("me/measurements")]
    [Authorize(Policy = Permissions.AthleteOnly)]
    public async Task<ActionResult<Result<PagedResult<MeasurementRecordDto>>>> MyMeasurements(
        [FromQuery] PagedRequest request,
        CancellationToken cancellationToken)
    {
        var athleteId = userContext.GetAthleteId();
        if (!athleteId.HasValue)
        {
            return BadRequest(Result<PagedResult<MeasurementRecordDto>>.Failure("Athlete profile not found."));
        }

        var result = await progressService.GetMeasurementsAsync(athleteId.Value, request, cancellationToken);
        return Ok(result);
    }
}
