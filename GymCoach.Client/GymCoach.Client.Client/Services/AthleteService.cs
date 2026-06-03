using System.Net.Http.Json;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public sealed class AthleteService(HttpClient httpClient) : IAthleteService
{
    public async Task<IReadOnlyList<AthleteDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await httpClient.GetFromJsonAsync<List<AthleteDto>>(ApiRoutes.Athletes.TrimStart('/'), cancellationToken);
        return result ?? [];
    }

    public async Task<AthleteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var athletes = await GetAllAsync(cancellationToken);
        return athletes.FirstOrDefault(a => a.Id == id);
    }
}
