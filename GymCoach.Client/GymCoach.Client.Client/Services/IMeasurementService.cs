using GymCoach.Shared.Dtos;

namespace GymCoach.Client.Client.Services;

public interface IMeasurementService
{
    Task<IReadOnlyList<MeasurementDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
