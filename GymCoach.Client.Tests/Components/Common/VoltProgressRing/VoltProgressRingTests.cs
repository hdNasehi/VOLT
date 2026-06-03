using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltProgressRingComponent = GymCoach.Client.Client.Components.Common.VoltProgressRing.VoltProgressRing;

namespace GymCoach.Client.Tests.Components.Common.VoltProgressRingTests;

public class VoltProgressRingTests : VoltComponentTestBase
{
    [Fact]
    public void Exposes_Progressbar_Role_And_Value()
    {
        var cut = RenderComponent<VoltProgressRingComponent>(p => p
            .Add(x => x.Value, 75d)
            .Add(x => x.AriaLabel, "Workout progress"));

        cut.Markup.Should().Contain("role=\"progressbar\"");
        cut.Markup.Should().Contain("aria-valuenow=\"75\"");
        cut.Markup.Should().Contain("aria-label=\"Workout progress\"");
    }
}
