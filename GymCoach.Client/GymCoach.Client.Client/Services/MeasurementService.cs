using System.Net.Http.Json;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public sealed class MeasurementService(HttpClient httpClient) : IMeasurementService
{
    public async Task<IReadOnlyList<MeasurementDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetFromJsonAsync<List<MeasurementDto>>(ApiRoutes.Measurements.TrimStart('/'), cancellationToken);
        return result ?? [];
    }
}
