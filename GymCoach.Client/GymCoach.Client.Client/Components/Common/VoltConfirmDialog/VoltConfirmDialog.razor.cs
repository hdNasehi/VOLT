using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Common.VoltConfirmDialog;

public partial class VoltConfirmDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter] public string Title { get; set; } = "Confirm";
    [Parameter] public string Message { get; set; } = string.Empty;
    [Parameter] public string ConfirmText { get; set; } = "Confirm";
    [Parameter] public string CancelText { get; set; } = "Cancel";

    private void Cancel() => MudDialog.Cancel();
    private void Confirm() => MudDialog.Close(DialogResult.Ok(true));
}
