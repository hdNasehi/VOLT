using GymCoach.Client.Client.Services;
using GymCoach.Client.Client.Services.Athlete;
using GymCoach.Client.Client.Services.Auth;
using GymCoach.Client.Client.Services.Localization;
using GymCoach.Client.Client.Services.Offline;
using Microsoft.AspNetCore.Components.Authorization;

namespace GymCoach.Client.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGymCoachClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiBaseUrl = configuration["ApiBaseUrl"] ?? "https://localhost:7219";

        services.AddSingleton<ILocalizationService, LocalizationService>();
        services.AddLocalization();

        services.AddAuthorizationCore();
        services.AddScoped<IAuthStateService, AuthStateService>();
        services.AddScoped<VoltAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<VoltAuthenticationStateProvider>());
        services.AddScoped<ILocalStorageService, LocalStorageService>();
        services.AddScoped<IAuthClientService, AuthClientService>();

        services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(apiBaseUrl.TrimEnd('/') + "/")
        });

        services.AddScoped<IOfflineSyncQueue, OfflineSyncQueue>();
        services.AddScoped<IAthleteService, AthleteService>();
        services.AddScoped<ICoachService, CoachService>();
        services.AddScoped<IExerciseService, ExerciseService>();
        services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
        services.AddScoped<IWorkoutTrackingService, WorkoutTrackingService>();
        services.AddScoped<IMeasurementService, MeasurementService>();
        services.AddScoped<IAiCoachService, AiCoachService>();
        services.AddScoped<IAthleteExperienceService, AthleteExperienceService>();

        return services;
    }
}
