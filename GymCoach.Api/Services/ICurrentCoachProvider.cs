using System.Security.Claims;

namespace GymCoach.Api.Services;

public interface ICurrentCoachProvider
{
    Guid GetCoachId();
    Guid? TryGetCoachId();
}

public class ConfigCurrentCoachProvider(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : ICurrentCoachProvider
{
    public Guid GetCoachId() =>
        TryGetCoachId()
        ?? Guid.Parse(configuration["DefaultCoachId"] ?? "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

    public Guid? TryGetCoachId()
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirstValue("coach_id");
        if (Guid.TryParse(claim, out var coachId))
        {
            return coachId;
        }

        return null;
    }
}
