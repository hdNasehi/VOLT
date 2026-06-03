using GymCoach.Shared.Common;
using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public interface ICoachService
{
    Task<IReadOnlyList<CoachDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<CoachDashboardDto>?> GetDashboardAsync(CancellationToken cancellationToken = default);
}