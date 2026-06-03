using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltStatCardComponent = GymCoach.Client.Client.Components.Common.VoltStatCard.VoltStatCard;

namespace GymCoach.Client.Tests.Components.Common.VoltStatCardTests;

public class VoltStatCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Label_And_Value()
    {
        var cut = RenderComponent<VoltStatCardComponent>(p => p
            .Add(x => x.Label, "Athletes")
            .Add(x => x.Value, "12"));

        cut.Markup.Should().Contain("Athletes");
        cut.Markup.Should().Contain("12");
    }
}
