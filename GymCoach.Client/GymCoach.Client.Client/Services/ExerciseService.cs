using System.Net.Http.Json;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public sealed class ExerciseService(HttpClient httpClient) : IExerciseService
{
    public async Task<IReadOnlyList<ExerciseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetFromJsonAsync<List<ExerciseDto>>(ApiRoutes.Exercises.TrimStart('/'), cancellationToken);
        return result ?? [];
    }
}
