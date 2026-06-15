using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Athlete.MeasurementCard;

public partial class MeasurementCard
{
    [Parameter, EditorRequired] public BodyProgressModel Progress { get; set; } = new();
    [Parameter] public string Title { get; set; } = "Body Progress";
    [Parameter] public string WeightLabel { get; set; } = "Weight";
    [Parameter] public string BodyFatLabel { get; set; } = "Body Fat";
    [Parameter] public string LastMeasuredLabel { get; set; } = "Last measured";
}
