using GymCoach.Shared.Sync;

namespace GymCoach.Client.Client.Services.Offline;

public sealed class OfflineSyncQueue : IOfflineSyncQueue
{
    private readonly List<PendingSyncOperation> _pending = [];

    public Task EnqueueAsync(PendingSyncOperation operation, CancellationToken cancellationToken = default)
    {
        _pending.Add(operation);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<PendingSyncOperation>> GetPendingAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<PendingSyncOperation>>(_pending.ToList());

    public Task MarkSyncedAsync(Guid operationId, CancellationToken cancellationToken = default)
    {
        _pending.RemoveAll(o => o.Id == operationId);
        return Task.CompletedTask;
    }
}
