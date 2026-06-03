using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Common.VoltProgressRing;

public partial class VoltProgressRing
{
    [Parameter] public double Value { get; set; }
    [Parameter] public bool ShowLabel { get; set; } = true;
    [Parameter] public Size Size { get; set; } = Size.Medium;
    [Parameter] public string AriaLabel { get; set; } = "Progress";
    [Parameter] public string? CssClass { get; set; }
}
