using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using WorkoutSessionTimerComponent = GymCoach.Client.Client.Components.Athlete.WorkoutSessionTimer.WorkoutSessionTimer;

namespace GymCoach.Client.Tests.Components.Athlete.WorkoutSessionTimer;

public class WorkoutSessionTimerTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Session_Timer()
    {
        var cut = RenderComponent<WorkoutSessionTimerComponent>(p => p
            .Add(x => x.StartedAt, DateTime.UtcNow.AddMinutes(-1))
            .Add(x => x.Label, "Session"));

        cut.Markup.Should().Contain("Session");
        cut.Markup.Should().Contain("00:");
    }
}
