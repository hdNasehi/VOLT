using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public interface IWorkoutPlanService
{
    Task<IReadOnlyList<WorkoutPlanDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
