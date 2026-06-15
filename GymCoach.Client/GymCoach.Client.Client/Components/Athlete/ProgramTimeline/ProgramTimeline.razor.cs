using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Athlete.ProgramTimeline;

public partial class ProgramTimeline
{
    [Parameter, EditorRequired] public IReadOnlyList<WorkoutDaySummaryModel> Days { get; set; } = [];
    [Parameter] public EventCallback<Guid> OnDaySelected { get; set; }
}
