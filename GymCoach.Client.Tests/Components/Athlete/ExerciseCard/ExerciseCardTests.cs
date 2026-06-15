using Bunit;
using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Microsoft.AspNetCore.Components;
using Xunit;
using ExerciseCardComponent = GymCoach.Client.Client.Components.Athlete.ExerciseCard.ExerciseCard;

namespace GymCoach.Client.Tests.Components.Athlete.ExerciseCard;

public class ExerciseCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Exercise_Details()
    {
        var exercise = new ExerciseDetailModel
        {
            Name = "Bench Press",
            MuscleGroup = "Chest",
            Sets = 4,
            Reps = 8,
            RestSeconds = 90,
            Tips = ["Keep shoulders back"],
            CoachNotes = "Control the descent"
        };

        var cut = RenderComponent<ExerciseCardComponent>(p => p.Add(x => x.Exercise, exercise));

        cut.Markup.Should().Contain("Bench Press");
        cut.Markup.Should().Contain("Chest");
        cut.Markup.Should().Contain("4 x 8");
        cut.Markup.Should().Contain("Control the descent");
    }

    [Fact]
    public void Invokes_OnComplete_When_Actions_Shown()
    {
        Guid? completed = null;
        var id = Guid.NewGuid();
        var exercise = new ExerciseDetailModel { Id = id, Name = "Curl" };

        var cut = RenderComponent<ExerciseCardComponent>(p => p
            .Add(x => x.Exercise, exercise)
            .Add(x => x.ShowActions, true)
            .Add(x => x.CompleteLabel, "Complete")
            .Add(x => x.OnComplete, EventCallback.Factory.Create<Guid>(this, g => completed = g)));

        cut.FindAll("button").Last().Click();
        completed.Should().Be(id);
    }
}
