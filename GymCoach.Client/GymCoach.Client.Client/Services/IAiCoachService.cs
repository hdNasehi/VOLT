using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public interface IAiCoachService
{
    Task<IReadOnlyList<AiCoachMessageDto>> GetHistoryAsync(Guid athleteId, CancellationToken cancellationToken = default);
}
