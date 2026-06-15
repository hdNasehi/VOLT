using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Athlete.ExerciseCard;

public partial class ExerciseCard
{
    [Parameter, EditorRequired] public ExerciseDetailModel Exercise { get; set; } = new();
    [Parameter] public bool ShowActions { get; set; }
    [Parameter] public bool IsLocked { get; set; }
    [Parameter] public bool IsCompleted { get; set; }
    [Parameter] public string TipsLabel { get; set; } = "Tips";
    [Parameter] public string MistakesLabel { get; set; } = "Common mistakes";
    [Parameter] public string RestLabel { get; set; } = "Rest";
    [Parameter] public string StartLabel { get; set; } = "Start";
    [Parameter] public string CompleteLabel { get; set; } = "Complete";
    [Parameter] public string CompletedLabel { get; set; } = "Done";
    [Parameter] public string LockedLabel { get; set; } = "Complete previous exercise first";
    [Parameter] public EventCallback<Guid> OnStart { get; set; }
    [Parameter] public EventCallback<Guid> OnComplete { get; set; }
}
