using Bunit;
using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Xunit;
using ActiveProgramCardComponent = GymCoach.Client.Client.Components.Athlete.ActiveProgramCard.ActiveProgramCard;

namespace GymCoach.Client.Tests.Components.Athlete.ActiveProgramCard;

public class ActiveProgramCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Program_Details()
    {
        var program = new ActiveProgramModel
        {
            ProgramName = "Hypertrophy A",
            CoachName = "Coach",
            CurrentDay = 3,
            TotalDays = 30,
            ProgressPercent = 25,
            DaysRemaining = 20
        };

        var cut = RenderComponent<ActiveProgramCardComponent>(p => p
            .Add(x => x.Program, program)
            .Add(x => x.StartWorkoutLabel, "Start"));

        cut.Markup.Should().Contain("Hypertrophy A");
        cut.Markup.Should().Contain("3 / 30");
        cut.Markup.Should().Contain("Start");
    }

    [Fact]
    public void Invokes_OnStartWorkout_When_Clicked()
    {
        var invoked = false;
        var program = new ActiveProgramModel { ProgramName = "Test" };

        var cut = RenderComponent<ActiveProgramCardComponent>(p => p
            .Add(x => x.Program, program)
            .Add(x => x.OnStartWorkout, () => { invoked = true; return Task.CompletedTask; }));

        cut.Find("button").Click();
        invoked.Should().BeTrue();
    }
}
