using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Xunit;
using AthleteDashboardHeaderComponent = GymCoach.Client.Client.Components.Athlete.AthleteDashboardHeader.AthleteDashboardHeader;

namespace GymCoach.Client.Tests.Components.Athlete.AthleteDashboardHeader;

public class AthleteDashboardHeaderTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Athlete_And_Coach_Info()
    {
        var cut = RenderComponent<AthleteDashboardHeaderComponent>(p => p
            .Add(x => x.AthleteName, "Ali")
            .Add(x => x.CoachName, "Amir")
            .Add(x => x.GymName, "Volt Gym")
            .Add(x => x.WelcomeLabel, "Welcome"));

        cut.Markup.Should().Contain("Ali");
        cut.Markup.Should().Contain("Amir");
        cut.Markup.Should().Contain("Volt Gym");
    }
}
