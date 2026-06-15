using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Athlete.RestTimer;

public partial class RestTimer : IDisposable
{
    private int _remaining;
    private bool _exceeded;
    private Timer? _timer;

    [Parameter] public string Title { get; set; } = "Rest";
    [Parameter] public int TotalSeconds { get; set; } = 60;
    [Parameter] public int? SecondsRemaining { get; set; }
    [Parameter] public bool IsActive { get; set; }
    [Parameter] public string ExceededLabel { get; set; } = "Rest exceeded";
    [Parameter] public string SkipLabel { get; set; } = "Skip rest";
    [Parameter] public EventCallback<int> OnTick { get; set; }
    [Parameter] public EventCallback OnSkip { get; set; }

    protected override void OnParametersSet()
    {
        _remaining = SecondsRemaining ?? TotalSeconds;
        _exceeded = _remaining < 0;
        RestartTimer();
    }

    private void RestartTimer()
    {
        _timer?.Dispose();
        if (!IsActive)
        {
            return;
        }

        _timer = new Timer(_ =>
        {
            _remaining--;
            if (_remaining < 0)
            {
                _exceeded = true;
            }

            InvokeAsync(async () =>
            {
                await OnTick.InvokeAsync(_remaining);
                StateHasChanged();
            });
        }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
    }

    private Task SkipRest() => OnSkip.InvokeAsync();

    private static string FormatTime(int seconds)
    {
        var abs = Math.Abs(seconds);
        return $"{abs / 60:D2}:{abs % 60:D2}";
    }

    public void Dispose() => _timer?.Dispose();
}
