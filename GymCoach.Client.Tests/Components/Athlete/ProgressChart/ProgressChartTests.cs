using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Xunit;
using ProgressChartComponent = GymCoach.Client.Client.Components.Athlete.ProgressChart.ProgressChart;

namespace GymCoach.Client.Tests.Components.Athlete.ProgressChart;

public class ProgressChartTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Chart_Points()
    {
        var points = new List<ChartPointModel>
        {
            new() { Label = "W1", Value = 80 },
            new() { Label = "W2", Value = 79 }
        };

        var cut = RenderComponent<ProgressChartComponent>(p => p
            .Add(x => x.Title, "Weight")
            .Add(x => x.Points, points));

        cut.Markup.Should().Contain("W1");
        cut.Markup.Should().Contain("80.0");
    }

    [Fact]
    public void Renders_Empty_State()
    {
        var cut = RenderComponent<ProgressChartComponent>(p => p
            .Add(x => x.Points, Array.Empty<ChartPointModel>())
            .Add(x => x.EmptyLabel, "No data"));

        cut.Markup.Should().Contain("No data");
    }
}
