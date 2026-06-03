using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public interface IWorkoutTrackingService
{
    Task<IReadOnlyList<WorkoutSessionDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
