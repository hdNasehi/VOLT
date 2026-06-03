using System.Net.Http.Json;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public sealed class WorkoutTrackingService(HttpClient httpClient) : IWorkoutTrackingService
{
    public async Task<IReadOnlyList<WorkoutSessionDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetFromJsonAsync<List<WorkoutSessionDto>>(ApiRoutes.WorkoutTracking.TrimStart('/'), cancellationToken);
        return result ?? [];
    }
}
