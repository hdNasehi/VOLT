using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltButtonComponent = GymCoach.Client.Client.Components.Common.VoltButton.VoltButton;

namespace GymCoach.Client.Tests.Components.Common.VoltButtonTests;

public class VoltButtonTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_ChildContent()
    {
        var cut = RenderComponent<VoltButtonComponent>(p => p
            .Add(x => x.ChildContent, "Save")
            .Add(x => x.Disabled, false));

        cut.Markup.Should().Contain("Save");
    }

    [Fact]
    public void Renders_Disabled_State()
    {
        var cut = RenderComponent<VoltButtonComponent>(p => p
            .Add(x => x.Disabled, true)
            .Add(x => x.ChildContent, "Disabled"));

        cut.Markup.Should().Contain("disabled");
    }
}
