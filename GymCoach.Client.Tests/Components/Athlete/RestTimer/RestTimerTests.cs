using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using RestTimerComponent = GymCoach.Client.Client.Components.Athlete.RestTimer.RestTimer;

namespace GymCoach.Client.Tests.Components.Athlete.RestTimer;

public class RestTimerTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Initial_Time()
    {
        var cut = RenderComponent<RestTimerComponent>(p => p
            .Add(x => x.TotalSeconds, 90)
            .Add(x => x.IsActive, false)
            .Add(x => x.Title, "Rest"));

        cut.Markup.Should().Contain("01:30");
        cut.Markup.Should().Contain("Rest");
    }
}
