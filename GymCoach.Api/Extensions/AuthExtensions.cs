using GymCoach.Api.Options;
using GymCoach.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GymCoach.Api.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddVoltAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwt = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? new JwtSettings();
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy(Permissions.SuperAdminOnly, p => p.RequireRole(Roles.SuperAdmin))
            .AddPolicy(Permissions.SystemAdminOrAbove, p => p.RequireRole(Roles.SuperAdmin, Roles.SystemAdmin))
            .AddPolicy(Permissions.GymManagerOrAbove, p => p.RequireRole(Roles.SuperAdmin, Roles.SystemAdmin, Roles.GymManager))
            .AddPolicy(Permissions.CoachOnly, p => p.RequireRole(Roles.Coach))
            .AddPolicy(Permissions.AthleteOnly, p => p.RequireRole(Roles.Athlete))
            .AddPolicy(Permissions.ManageExercises, p => p.RequireRole(Roles.SuperAdmin, Roles.SystemAdmin))
            .AddPolicy(Permissions.ApproveCoaches, p => p.RequireRole(Roles.SuperAdmin, Roles.SystemAdmin))
            .AddPolicy(Permissions.ManageSettlements, p => p.RequireRole(Roles.SuperAdmin, Roles.SystemAdmin))
            .AddPolicy(Permissions.ManagePlatformSettings, p => p.RequireRole(Roles.SuperAdmin));

        return services;
    }
}
