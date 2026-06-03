using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public interface IAthleteService
{
    Task<IReadOnlyList<AthleteDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AthleteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
