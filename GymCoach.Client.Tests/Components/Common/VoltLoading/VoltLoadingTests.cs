using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Xunit;
using VoltLoadingComponent = GymCoach.Client.Client.Components.Common.VoltLoading.VoltLoading;

namespace GymCoach.Client.Tests.Components.Common.VoltLoadingTests;

public class VoltLoadingTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Status_Message()
    {
        var cut = RenderComponent<VoltLoadingComponent>(p => p.Add(x => x.Message, "Loading athletes"));
        cut.Markup.Should().Contain("Loading athletes");
        cut.Markup.Should().Contain("role=\"status\"");
    }
}
