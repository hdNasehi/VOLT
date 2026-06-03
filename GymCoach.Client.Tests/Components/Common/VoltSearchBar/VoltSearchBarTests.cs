using FluentAssertions;
using GymCoach.Client.Tests.Support;
using Microsoft.AspNetCore.Components;
using Xunit;
using VoltSearchBarComponent = GymCoach.Client.Client.Components.Common.VoltSearchBar.VoltSearchBar;

namespace GymCoach.Client.Tests.Components.Common.VoltSearchBarTests;

public class VoltSearchBarTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Search_Placeholder()
    {
        var cut = RenderComponent<VoltSearchBarComponent>(p => p
            .Add(x => x.Placeholder, "Search athletes")
            .Add(x => x.OnSearch, EventCallback.Factory.Create<string>(this, _ => Task.CompletedTask)));

        cut.Markup.Should().Contain("Search athletes");
    }
}
