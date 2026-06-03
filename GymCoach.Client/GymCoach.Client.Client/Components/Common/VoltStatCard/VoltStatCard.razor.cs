using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Common.VoltStatCard;

public partial class VoltStatCard
{
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public string? Subtitle { get; set; }
    [Parameter] public string? Icon { get; set; }
}
