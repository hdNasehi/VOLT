using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Common.VoltPageHeader;

public partial class VoltPageHeader
{
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string? Subtitle { get; set; }
    [Parameter] public string? CssClass { get; set; }
}
