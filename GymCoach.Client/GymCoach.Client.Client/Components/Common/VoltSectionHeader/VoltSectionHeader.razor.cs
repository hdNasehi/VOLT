using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Common.VoltSectionHeader;

public partial class VoltSectionHeader
{
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public RenderFragment? Actions { get; set; }
    [Parameter] public string? CssClass { get; set; }
}
