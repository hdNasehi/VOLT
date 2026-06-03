using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public interface IExerciseService
{
    Task<IReadOnlyList<ExerciseDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
