using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Common.VoltLoading;

public partial class VoltLoading
{
    [Parameter] public string Message { get; set; } = "Loading...";
    [Parameter] public string AriaLabel { get; set; } = "Loading";
    [Parameter] public Size Size { get; set; } = Size.Medium;
    [Parameter] public string? CssClass { get; set; }
}
