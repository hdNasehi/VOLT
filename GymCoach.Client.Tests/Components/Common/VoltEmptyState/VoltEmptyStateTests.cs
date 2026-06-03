using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltEmptyStateComponent = GymCoach.Client.Client.Components.Common.VoltEmptyState.VoltEmptyState;

namespace GymCoach.Client.Tests.Components.Common.VoltEmptyStateTests;

public class VoltEmptyStateTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Title_And_Description()
    {
        var cut = RenderComponent<VoltEmptyStateComponent>(p => p
            .Add(x => x.Title, "Empty")
            .Add(x => x.Description, "No items"));

        cut.Markup.Should().Contain("Empty");
        cut.Markup.Should().Contain("No items");
    }
}
