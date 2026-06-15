using Bunit;
using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Microsoft.AspNetCore.Components;
using GymCoach.Shared.Enums;
using Xunit;
using ProgramSummaryCardComponent = GymCoach.Client.Client.Components.Athlete.ProgramSummaryCard.ProgramSummaryCard;

namespace GymCoach.Client.Tests.Components.Athlete.ProgramSummaryCard;

public class ProgramSummaryCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Program_Summary()
    {
        var program = new ProgramSummaryModel
        {
            Name = "Block A",
            CoachName = "Coach",
            ProgressPercent = 50,
            Status = ProgramStatus.Active
        };

        var cut = RenderComponent<ProgramSummaryCardComponent>(p => p.Add(x => x.Program, program));

        cut.Markup.Should().Contain("Block A");
        cut.Markup.Should().Contain("50%");
    }

    [Fact]
    public void Invokes_OnSelected()
    {
        Guid? selected = null;
        var id = Guid.NewGuid();
        var program = new ProgramSummaryModel { Id = id, Name = "Test" };

        var cut = RenderComponent<ProgramSummaryCardComponent>(p => p
            .Add(x => x.Program, program)
            .Add(x => x.OnSelected, EventCallback.Factory.Create<Guid>(this, g => selected = g)));

        cut.Find(".program-summary-card").Click();
        selected.Should().Be(id);
    }
}
