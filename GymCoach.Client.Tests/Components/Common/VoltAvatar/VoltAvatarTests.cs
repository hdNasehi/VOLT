using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltAvatarComponent = GymCoach.Client.Client.Components.Common.VoltAvatar.VoltAvatar;

namespace GymCoach.Client.Tests.Components.Common.VoltAvatarTests;

public class VoltAvatarTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Initials_When_No_Image()
    {
        var cut = RenderComponent<VoltAvatarComponent>(p => p.Add(x => x.Initials, "AR"));
        cut.Markup.Should().Contain("AR");
    }

    [Fact]
    public void Has_Accessibility_Label()
    {
        var cut = RenderComponent<VoltAvatarComponent>(p => p.Add(x => x.AriaLabel, "Alex Rivera"));
        cut.Markup.Should().Contain("aria-label=\"Alex Rivera\"");
    }
}
