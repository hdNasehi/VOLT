namespace GymCoach.Shared.Constants;

public static class ApiRoutes
{
    public const string ApiBase = "/api";

    public const string Athletes = $"{ApiBase}/athletes";
    public const string Coaches = $"{ApiBase}/coaches";
    public const string Exercises = $"{ApiBase}/exercises";
    public const string WorkoutPlans = $"{ApiBase}/workout-plans";
    public const string WorkoutTracking = $"{ApiBase}/workout-tracking";
    public const string Measurements = $"{ApiBase}/measurements";
    public const string AiCoach = $"{ApiBase}/ai-coach";
    public const string Coach = $"{ApiBase}/coach";
    public const string CoachDashboard = $"{Coach}/dashboard";
    public const string Auth = $"{ApiBase}/auth";
    public const string Health = "/health";
}
