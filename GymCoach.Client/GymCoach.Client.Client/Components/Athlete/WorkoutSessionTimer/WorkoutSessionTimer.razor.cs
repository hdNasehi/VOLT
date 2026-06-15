using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Athlete.WorkoutSessionTimer;

public partial class WorkoutSessionTimer : IDisposable
{
    private TimeSpan _elapsed;
    private Timer? _timer;

    [Parameter] public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    [Parameter] public string Label { get; set; } = "Session time";

    protected override void OnInitialized()
    {
        _elapsed = DateTime.UtcNow - StartedAt;
        _timer = new Timer(_ => InvokeAsync(() =>
        {
            _elapsed = DateTime.UtcNow - StartedAt;
            StateHasChanged();
        }), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    public void Dispose() => _timer?.Dispose();
}
