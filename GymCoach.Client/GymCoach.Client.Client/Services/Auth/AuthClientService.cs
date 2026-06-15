using System.Net.Http.Json;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Utilities;

namespace GymCoach.Client.Client.Services.Auth;

public interface IAuthClientService
{
    Task<Result<AuthTokenResponse>> LoginAsync(AuthLoginRequest request, CancellationToken cancellationToken = default);
}

public sealed class AuthClientService(HttpClient httpClient) : IAuthClientService
{
    public const string TestPhone = "09120000000";
    public const string TestPassword = "Test123!";

    public async Task<Result<AuthTokenResponse>> LoginAsync(
        AuthLoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var phone = PhoneNormalizer.Normalize(request.PhoneNumber);

        if (phone == TestPhone && request.Password == TestPassword)
        {
            return Result<AuthTokenResponse>.Success(CreateTestAthleteSession());
        }

        try
        {
            var response = await httpClient.PostAsJsonAsync("api/auth/login", request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return Result<AuthTokenResponse>.Failure("Invalid phone number or password.");
            }

            var result = await response.Content.ReadFromJsonAsync<Result<AuthTokenResponse>>(cancellationToken);
            return result ?? Result<AuthTokenResponse>.Failure("Unexpected login response.");
        }
        catch (HttpRequestException)
        {
            if (phone == TestPhone && request.Password == TestPassword)
            {
                return Result<AuthTokenResponse>.Success(CreateTestAthleteSession());
            }

            return Result<AuthTokenResponse>.Failure("Unable to reach the API. Use the test athlete account offline.");
        }
    }

    private static AuthTokenResponse CreateTestAthleteSession() => new()
    {
        AccessToken = "test-athlete-token",
        ExpiresAt = DateTime.UtcNow.AddHours(8),
        User = new UserProfileDto
        {
            UserId = "test-athlete-user",
            PhoneNumber = TestPhone,
            FullName = "علی محمودی",
            Roles = [Roles.Athlete],
            AthleteId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        }
    };
}
