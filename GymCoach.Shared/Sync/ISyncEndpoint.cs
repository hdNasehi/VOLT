namespace GymCoach.Shared.Sync;

public interface ISyncEndpoint
{
    Task PushAsync(IReadOnlyList<PendingSyncOperation> operations, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PendingSyncOperation>> PullAsync(DateTime? since, CancellationToken cancellationToken = default);
}
