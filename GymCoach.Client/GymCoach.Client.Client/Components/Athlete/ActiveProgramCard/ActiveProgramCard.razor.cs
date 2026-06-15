using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GymCoach.Client.Client.Components.Athlete.ActiveProgramCard;

public partial class ActiveProgramCard
{
    [Parameter, EditorRequired] public ActiveProgramModel Program { get; set; } = new();
    [Parameter] public string Title { get; set; } = "Active Program";
    [Parameter] public string CoachLabel { get; set; } = "Coach";
    [Parameter] public string DayLabel { get; set; } = "Day";
    [Parameter] public string RemainingLabel { get; set; } = "Days left";
    [Parameter] public string ProgressLabel { get; set; } = "Progress";
    [Parameter] public string StartWorkoutLabel { get; set; } = "Start Workout";
    [Parameter] public EventCallback<MouseEventArgs> OnStartWorkout { get; set; }

    private Task HandleStartAsync(MouseEventArgs args) => OnStartWorkout.InvokeAsync(args);
}
