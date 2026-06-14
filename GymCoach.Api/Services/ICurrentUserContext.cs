using System.Security.Claims;

namespace GymCoach.Api.Services;

public interface ICurrentUserContext
{
    string? GetUserId();
    Guid? GetCoachId();
    Guid? GetAthleteId();
}

public sealed class HttpCurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{
    public string? GetUserId() =>
        httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? httpContextAccessor.HttpContext?.User.FindFirstValue("sub");

    public Guid? GetCoachId()
    {
        var value = httpContextAccessor.HttpContext?.User.FindFirstValue("coach_id");
        return Guid.TryParse(value, out var id) ? id : null;
    }

    public Guid? GetAthleteId()
    {
        var value = httpContextAccessor.HttpContext?.User.FindFirstValue("athlete_id");
        return Guid.TryParse(value, out var id) ? id : null;
    }
}
