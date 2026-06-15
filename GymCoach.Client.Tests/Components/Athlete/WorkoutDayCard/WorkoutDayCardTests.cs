using Bunit;
using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Microsoft.AspNetCore.Components;
using Xunit;
using WorkoutDayCardComponent = GymCoach.Client.Client.Components.Athlete.WorkoutDayCard.WorkoutDayCard;

namespace GymCoach.Client.Tests.Components.Athlete.WorkoutDayCard;

public class WorkoutDayCardTests : VoltComponentTestBase
{
    [Fact]
    public void Applies_Current_State_Class()
    {
        var day = new WorkoutDaySummaryModel { Name = "Day 2", State = WorkoutDayState.Current };

        var cut = RenderComponent<WorkoutDayCardComponent>(p => p.Add(x => x.Day, day));

        cut.Find(".workout-day-card").ClassList.Should().Contain("is-current");
    }

    [Fact]
    public void Invokes_OnSelected()
    {
        Guid? selected = null;
        var id = Guid.NewGuid();
        var day = new WorkoutDaySummaryModel { Id = id, Name = "Day 1" };

        var cut = RenderComponent<WorkoutDayCardComponent>(p => p
            .Add(x => x.Day, day)
            .Add(x => x.OnSelected, EventCallback.Factory.Create<Guid>(this, g => selected = g)));

        cut.Find(".workout-day-card").Click();
        selected.Should().Be(id);
    }
}
