using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Athlete.PreviousProgramsSection;

public partial class PreviousProgramsSection
{
    [Parameter] public string Title { get; set; } = "Previous Programs";
    [Parameter] public string EmptyLabel { get; set; } = "No previous programs";
    [Parameter] public string CoachLabel { get; set; } = "Coach";
    [Parameter] public string ActiveLabel { get; set; } = "Active";
    [Parameter] public string CompletedLabel { get; set; } = "Completed";
    [Parameter] public IReadOnlyList<ProgramSummaryModel> Programs { get; set; } = [];
    [Parameter] public EventCallback<Guid> OnProgramSelected { get; set; }
}
