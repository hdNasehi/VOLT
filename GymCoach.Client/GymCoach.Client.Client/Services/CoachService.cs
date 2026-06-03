using System.Net.Http.Json;
using GymCoach.Shared.Common;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public sealed class CoachService(HttpClient httpClient) : ICoachService
{
    public async Task<IReadOnlyList<CoachDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetFromJsonAsync<List<CoachDto>>(ApiRoutes.Coaches.TrimStart('/'), cancellationToken);
        return result ?? [];
    }

    public async Task<Result<CoachDashboardDto>?> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<Result<CoachDashboardDto>>(
            ApiRoutes.CoachDashboard.TrimStart('/'),
            cancellationToken);
    }
}