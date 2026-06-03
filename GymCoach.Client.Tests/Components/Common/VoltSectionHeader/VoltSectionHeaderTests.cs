using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltSectionHeaderComponent = GymCoach.Client.Client.Components.Common.VoltSectionHeader.VoltSectionHeader;

namespace GymCoach.Client.Tests.Components.Common.VoltSectionHeaderTests;

public class VoltSectionHeaderTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Title()
    {
        var cut = RenderComponent<VoltSectionHeaderComponent>(p => p.Add(x => x.Title, "Recent"));
        cut.Markup.Should().Contain("Recent");
    }
}
