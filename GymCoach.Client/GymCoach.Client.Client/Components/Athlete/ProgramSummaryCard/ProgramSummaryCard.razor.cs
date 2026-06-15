using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Athlete.ProgramSummaryCard;

public partial class ProgramSummaryCard
{
    [Parameter, EditorRequired] public ProgramSummaryModel Program { get; set; } = new();
    [Parameter] public string CoachLabel { get; set; } = "Coach";
    [Parameter] public string ActiveLabel { get; set; } = "Active";
    [Parameter] public string CompletedLabel { get; set; } = "Completed";
    [Parameter] public EventCallback<Guid> OnSelected { get; set; }

    private string StatusClass => Program.IsActive ? "is-active" : "is-completed";

    private Color StatusColor => Program.IsActive ? Color.Success : Color.Default;

    private string StatusText => Program.Status == ProgramStatus.Completed ? CompletedLabel : ActiveLabel;

    private Task HandleClick(MouseEventArgs _) => OnSelected.InvokeAsync(Program.Id);
}
