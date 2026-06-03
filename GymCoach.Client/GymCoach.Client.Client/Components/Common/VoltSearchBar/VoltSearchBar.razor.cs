using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GymCoach.Client.Client.Components.Common.VoltSearchBar;

public partial class VoltSearchBar
{
    private string _searchText = string.Empty;

    [Parameter] public string Placeholder { get; set; } = "Search...";
    [Parameter] public string AriaLabel { get; set; } = "Search";
    [Parameter] public string? CssClass { get; set; }
    [Parameter] public EventCallback<string> OnSearch { get; set; }

    private async Task OnSearchKeyUp(KeyboardEventArgs args)
    {
        if (OnSearch.HasDelegate)
        {
            await OnSearch.InvokeAsync(_searchText);
        }
    }
}
