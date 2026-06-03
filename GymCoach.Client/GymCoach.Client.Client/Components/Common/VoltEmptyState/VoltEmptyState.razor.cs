using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Common.VoltEmptyState;

public partial class VoltEmptyState
{
    [Parameter] public string Title { get; set; } = "No data";
    [Parameter] public string? Description { get; set; }
    [Parameter] public string? Icon { get; set; }
    [Parameter] public string? CssClass { get; set; }
    [Parameter] public RenderFragment? ActionContent { get; set; }
}
