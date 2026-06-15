using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GymCoach.Client.Client.Components.Athlete.WorkoutDayCard;

public partial class WorkoutDayCard
{
    [Parameter, EditorRequired] public WorkoutDaySummaryModel Day { get; set; } = new();
    [Parameter] public EventCallback<Guid> OnSelected { get; set; }

    private string StateClass => Day.State switch
    {
        WorkoutDayState.Current => "is-current",
        WorkoutDayState.Completed => "is-completed",
        WorkoutDayState.Missed => "is-missed",
        _ => "is-future"
    };

    private Task HandleClick(MouseEventArgs _) => OnSelected.InvokeAsync(Day.Id);
}
