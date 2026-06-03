using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltPageHeaderComponent = GymCoach.Client.Client.Components.Common.VoltPageHeader.VoltPageHeader;

namespace GymCoach.Client.Tests.Components.Common.VoltPageHeaderTests;

public class VoltPageHeaderTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Title_And_Subtitle()
    {
        var cut = RenderComponent<VoltPageHeaderComponent>(p => p
            .Add(x => x.Title, "Dashboard")
            .Add(x => x.Subtitle, "Overview"));

        cut.Markup.Should().Contain("Dashboard");
        cut.Markup.Should().Contain("Overview");
    }
}
