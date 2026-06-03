namespace GymCoach.Api.Services;

public interface ICurrentCoachProvider
{
    Guid GetCoachId();
}

public class ConfigCurrentCoachProvider(IConfiguration configuration) : ICurrentCoachProvider
{
    public Guid GetCoachId() =>
        Guid.Parse(configuration["DefaultCoachId"] ?? "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
}
