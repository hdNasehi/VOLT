using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Common.VoltButton;

public partial class VoltButton
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public Variant Variant { get; set; } = Variant.Filled;
    [Parameter] public Color Color { get; set; } = Color.Primary;
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string? CssClass { get; set; }
    [Parameter] public string? AriaLabel { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private async Task HandleClickAsync(MouseEventArgs args)
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }
    }
}
