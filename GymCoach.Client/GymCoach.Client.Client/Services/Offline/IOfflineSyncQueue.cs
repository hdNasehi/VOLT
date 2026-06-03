using GymCoach.Shared.Sync;

namespace GymCoach.Client.Client.Services.Offline;

public interface IOfflineSyncQueue
{
    Task EnqueueAsync(PendingSyncOperation operation, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PendingSyncOperation>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task MarkSyncedAsync(Guid operationId, CancellationToken cancellationToken = default);
}
