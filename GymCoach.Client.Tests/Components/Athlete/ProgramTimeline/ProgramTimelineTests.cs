using Bunit;
using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Xunit;
using ProgramTimelineComponent = GymCoach.Client.Client.Components.Athlete.ProgramTimeline.ProgramTimeline;

namespace GymCoach.Client.Tests.Components.Athlete.ProgramTimeline;

public class ProgramTimelineTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_All_Days()
    {
        var days = new List<WorkoutDaySummaryModel>
        {
            new() { Id = Guid.NewGuid(), Name = "Day 1", State = WorkoutDayState.Completed },
            new() { Id = Guid.NewGuid(), Name = "Day 2", State = WorkoutDayState.Current }
        };

        var cut = RenderComponent<ProgramTimelineComponent>(p => p.Add(x => x.Days, days));

        cut.Markup.Should().Contain("Day 1");
        cut.Markup.Should().Contain("Day 2");
    }
}
