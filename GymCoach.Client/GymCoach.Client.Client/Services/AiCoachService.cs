using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

/// <summary>Phase 1 stub — AI Coach is not implemented; returns empty history.</summary>
public sealed class AiCoachService : IAiCoachService
{
    public Task<IReadOnlyList<AiCoachMessageDto>> GetHistoryAsync(Guid athleteId, CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<AiCoachMessageDto>>([]);
}
