using System.Net.Http.Json;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public sealed class AiCoachService(HttpClient httpClient) : IAiCoachService
{
    public async Task<IReadOnlyList<AiCoachMessageDto>> GetHistoryAsync(Guid athleteId, CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetFromJsonAsync<List<AiCoachMessageDto>>(ApiRoutes.AiCoach.TrimStart('/'), cancellationToken);
        return result?.Where(m => m.AthleteId == athleteId).ToList() ?? [];
    }
}
