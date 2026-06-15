using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using GymCoach.Shared.Enums;
using Xunit;
using PreviousProgramsSectionComponent = GymCoach.Client.Client.Components.Athlete.PreviousProgramsSection.PreviousProgramsSection;

namespace GymCoach.Client.Tests.Components.Athlete.PreviousProgramsSection;

public class PreviousProgramsSectionTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Empty_State()
    {
        var cut = RenderComponent<PreviousProgramsSectionComponent>(p => p
            .Add(x => x.Programs, Array.Empty<ProgramSummaryModel>())
            .Add(x => x.EmptyLabel, "None"));

        cut.Markup.Should().Contain("None");
    }

    [Fact]
    public void Renders_Programs()
    {
        var programs = new List<ProgramSummaryModel>
        {
            new() { Name = "Old Block", Status = ProgramStatus.Completed }
        };

        var cut = RenderComponent<PreviousProgramsSectionComponent>(p => p.Add(x => x.Programs, programs));

        cut.Markup.Should().Contain("Old Block");
    }
}
