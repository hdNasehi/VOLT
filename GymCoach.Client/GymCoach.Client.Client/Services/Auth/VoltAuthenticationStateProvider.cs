using System.Security.Claims;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Components.Authorization;

namespace GymCoach.Client.Client.Services.Auth;

public sealed class VoltAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly IAuthStateService _authState;

    public VoltAuthenticationStateProvider(IAuthStateService authState)
    {
        _authState = authState;
        _authState.AuthStateChanged += OnAuthStateChanged;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = _authState.CurrentUser;
        if (user is null)
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        }

        return Task.FromResult(new AuthenticationState(CreatePrincipal(user)));
    }

    public void NotifyAuthenticationChanged() =>
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private void OnAuthStateChanged() => NotifyAuthenticationChanged();

    private static ClaimsPrincipal CreatePrincipal(UserProfileDto profile)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, profile.UserId),
            new(ClaimTypes.Name, profile.FullName),
            new(ClaimTypes.MobilePhone, profile.PhoneNumber)
        };

        foreach (var role in profile.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (profile.AthleteId.HasValue)
        {
            claims.Add(new Claim("athlete_id", profile.AthleteId.Value.ToString()));
        }

        if (profile.CoachId.HasValue)
        {
            claims.Add(new Claim("coach_id", profile.CoachId.Value.ToString()));
        }

        return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: "volt"));
    }

    public void Dispose() => _authState.AuthStateChanged -= OnAuthStateChanged;
}
