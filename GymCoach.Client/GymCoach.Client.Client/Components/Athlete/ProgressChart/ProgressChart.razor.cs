using GymCoach.Client.Client.Models.Athlete;
using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Athlete.ProgressChart;

public partial class ProgressChart
{
    [Parameter] public string Title { get; set; } = "Trend";
    [Parameter] public IReadOnlyList<ChartPointModel> Points { get; set; } = [];
    [Parameter] public string EmptyLabel { get; set; } = "No data yet";

    private static double GetBarHeight(decimal value)
    {
        var clamped = Math.Clamp((double)value, 0, 200);
        return Math.Max(8, clamped / 2);
    }
}
