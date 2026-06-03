using System.Net.Http.Json;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public sealed class WorkoutPlanService(HttpClient httpClient) : IWorkoutPlanService
{
    public async Task<IReadOnlyList<WorkoutPlanDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetFromJsonAsync<List<WorkoutPlanDto>>(ApiRoutes.WorkoutPlans.TrimStart('/'), cancellationToken);
        return result ?? [];
    }
}
