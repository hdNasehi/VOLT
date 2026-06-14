using GymCoach.Api.Options;
using GymCoach.Database.Entities;
using GymCoach.Database.Identity;
using GymCoach.Database.Repositories;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Utilities;
using GymCoach.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymCoach.Api.Services;

public interface IAuthService
{
    Task<Result<AuthTokenResponse>> RegisterAsync(AuthRegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthTokenResponse>> LoginAsync(AuthLoginRequest request, CancellationToken cancellationToken = default);
    Task<Result<UserProfileDto>> GetProfileAsync(string userId, CancellationToken cancellationToken = default);
}

public sealed class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ICoachRepository coachRepository,
    IAthleteRepository athleteRepository,
    IOptions<JwtSettings> jwtOptions) : IAuthService
{
    public async Task<Result<AuthTokenResponse>> RegisterAsync(
        AuthRegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var phone = PhoneNormalizer.Normalize(request.PhoneNumber);
        if (!PhoneNormalizer.IsValid(phone))
        {
            return Result<AuthTokenResponse>.Failure("Invalid phone number.");
        }

        if (await userManager.Users.AnyAsync(u => u.PhoneNumber == phone, cancellationToken))
        {
            return Result<AuthTokenResponse>.Failure("Phone number already registered.");
        }

        var user = new ApplicationUser
        {
            UserName = phone,
            PhoneNumber = phone,
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim()
        };

        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return Result<AuthTokenResponse>.Failure(string.Join("; ", createResult.Errors.Select(e => e.Description)));
        }

        var role = request.Role switch
        {
            Roles.Coach => Roles.Coach,
            Roles.GymManager => Roles.GymManager,
            Roles.SystemAdmin => Roles.SystemAdmin,
            Roles.SuperAdmin => Roles.SuperAdmin,
            _ => Roles.Athlete
        };

        await userManager.AddToRoleAsync(user, role);

        if (role == Roles.Coach)
        {
            await coachRepository.AddAsync(new Coach
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = phone,
                ApprovalStatus = CoachApprovalStatus.PendingApproval
            }, cancellationToken);
            await coachRepository.SaveChangesAsync(cancellationToken);
        }
        else if (role == Roles.Athlete)
        {
            var athlete = await athleteRepository.GetByUserIdAsync(user.Id, cancellationToken);
            if (athlete is null)
            {
                await athleteRepository.AddAthleteAsync(new Athlete
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = phone,
                    Email = string.Empty,
                    Status = AthleteStatus.Active
                }, cancellationToken);
                await athleteRepository.SaveChangesAsync(cancellationToken);
            }
        }

        return Result<AuthTokenResponse>.Success(await BuildTokenResponseAsync(user, cancellationToken));
    }

    public async Task<Result<AuthTokenResponse>> LoginAsync(
        AuthLoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var phone = PhoneNormalizer.Normalize(request.PhoneNumber);
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone, cancellationToken);
        if (user is null || !user.IsActive)
        {
            return Result<AuthTokenResponse>.Failure("Invalid credentials.");
        }

        var signIn = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!signIn.Succeeded)
        {
            return Result<AuthTokenResponse>.Failure("Invalid credentials.");
        }

        return Result<AuthTokenResponse>.Success(await BuildTokenResponseAsync(user, cancellationToken));
    }

    public async Task<Result<UserProfileDto>> GetProfileAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return Result<UserProfileDto>.Failure("User not found.");
        }

        return Result<UserProfileDto>.Success(await BuildProfileAsync(user, cancellationToken));
    }

    private async Task<AuthTokenResponse> BuildTokenResponseAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var profile = await BuildProfileAsync(user, cancellationToken);
        var jwt = jwtOptions.Value;
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.UniqueName, user.PhoneNumber ?? user.UserName ?? user.Id),
            new("full_name", profile.FullName)
        };

        foreach (var role in profile.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (profile.CoachId.HasValue)
        {
            claims.Add(new Claim("coach_id", profile.CoachId.Value.ToString()));
        }

        if (profile.AthleteId.HasValue)
        {
            claims.Add(new Claim("athlete_id", profile.AthleteId.Value.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(jwt.ExpirationMinutes);
        var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: expires, signingCredentials: creds);

        return new AuthTokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expires,
            User = profile
        };
    }

    private async Task<UserProfileDto> BuildProfileAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var roles = (await userManager.GetRolesAsync(user)).ToList();
        var coach = await coachRepository.GetByUserIdAsync(user.Id, cancellationToken);
        var athlete = await athleteRepository.GetByUserIdAsync(user.Id, cancellationToken);

        return new UserProfileDto
        {
            UserId = user.Id,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            FullName = $"{user.FirstName} {user.LastName}".Trim(),
            Roles = roles,
            CoachId = coach?.Id,
            AthleteId = athlete?.Id
        };
    }
}
