using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services.Auth;

public interface IAuthStateService
{
    UserProfileDto? CurrentUser { get; }
    string? AccessToken { get; }
    bool IsAuthenticated { get; }
    event Action? AuthStateChanged;
    Task InitializeAsync();
    Task SetSessionAsync(AuthTokenResponse response);
    Task SignOutAsync();
    string GetHomeRoute();
}

public sealed class AuthStateService : IAuthStateService
{
    private const string TokenKey = "volt.auth.token";
    private const string UserKey = "volt.auth.user";

    private readonly ILocalStorageService _storage;

    public AuthStateService(ILocalStorageService storage) => _storage = storage;

    public UserProfileDto? CurrentUser { get; private set; }
    public string? AccessToken { get; private set; }
    public bool IsAuthenticated => CurrentUser is not null;
    public event Action? AuthStateChanged;

    public async Task InitializeAsync()
    {
        AccessToken = await _storage.GetItemAsync(TokenKey);
        CurrentUser = await _storage.GetItemAsync<UserProfileDto>(UserKey);
        AuthStateChanged?.Invoke();
    }

    public async Task SetSessionAsync(AuthTokenResponse response)
    {
        AccessToken = response.AccessToken;
        CurrentUser = response.User;
        await _storage.SetItemAsync(TokenKey, response.AccessToken);
        await _storage.SetItemAsync(UserKey, response.User);
        AuthStateChanged?.Invoke();
    }

    public async Task SignOutAsync()
    {
        AccessToken = null;
        CurrentUser = null;
        await _storage.RemoveItemAsync(TokenKey);
        await _storage.RemoveItemAsync(UserKey);
        AuthStateChanged?.Invoke();
    }

    public string GetHomeRoute()
    {
        if (CurrentUser?.Roles is null || CurrentUser.Roles.Count == 0)
        {
            return "/login";
        }

        if (CurrentUser.Roles.Contains(Roles.SuperAdmin) || CurrentUser.Roles.Contains(Roles.SystemAdmin))
        {
            return "/admin";
        }

        if (CurrentUser.Roles.Contains(Roles.GymManager))
        {
            return "/gym";
        }

        if (CurrentUser.Roles.Contains(Roles.Coach))
        {
            return "/";
        }

        if (CurrentUser.Roles.Contains(Roles.Athlete))
        {
            return "/athlete";
        }

        return "/login";
    }
}
