using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Common.VoltAvatar;

public partial class VoltAvatar
{
    [Parameter] public string? ImageUrl { get; set; }
    [Parameter] public string Initials { get; set; } = "?";
    [Parameter] public string AltText { get; set; } = "Avatar";
    [Parameter] public string? AriaLabel { get; set; }
    [Parameter] public Size Size { get; set; } = Size.Medium;
    [Parameter] public Color Color { get; set; } = Color.Primary;
    [Parameter] public string? CssClass { get; set; }
}
