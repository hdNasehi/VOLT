namespace GymCoach.Api.Options;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";
    public string Key { get; set; } = "VOLT-Dev-Signing-Key-Change-In-Production-Min32Chars!";
    public string Issuer { get; set; } = "GymCoach.Api";
    public string Audience { get; set; } = "GymCoach.Client";
    public int ExpirationMinutes { get; set; } = 480;
}
