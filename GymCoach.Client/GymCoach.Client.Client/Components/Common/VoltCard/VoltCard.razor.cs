using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Common.VoltCard;

public partial class VoltCard
{
    [Parameter] public string? Title { get; set; }
    [Parameter] public int Elevation { get; set; } = 1;
    [Parameter] public string? CssClass { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? Actions { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
