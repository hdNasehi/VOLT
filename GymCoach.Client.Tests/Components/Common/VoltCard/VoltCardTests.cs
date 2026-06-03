using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltCardComponent = GymCoach.Client.Client.Components.Common.VoltCard.VoltCard;

namespace GymCoach.Client.Tests.Components.Common.VoltCardTests;

public class VoltCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Title_And_ChildContent()
    {
        var cut = RenderComponent<VoltCardComponent>(p => p
            .Add(x => x.Title, "Stats")
            .Add(x => x.ChildContent, "Body"));

        cut.Markup.Should().Contain("Stats");
        cut.Markup.Should().Contain("Body");
    }
}
