using Bunit;
using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Xunit;
using CoachMessagesCardComponent = GymCoach.Client.Client.Components.Athlete.CoachMessagesCard.CoachMessagesCard;

namespace GymCoach.Client.Tests.Components.Athlete.CoachMessagesCard;

public class CoachMessagesCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Message_And_Unread_Count()
    {
        var message = new CoachMessagePreviewModel
        {
            LastMessage = "Focus on form",
            UnreadCount = 2
        };

        var cut = RenderComponent<CoachMessagesCardComponent>(p => p
            .Add(x => x.Message, message)
            .Add(x => x.UnreadLabel, "unread"));

        cut.Markup.Should().Contain("Focus on form");
        cut.Markup.Should().Contain("2");
    }

    [Fact]
    public void Invokes_OnClick()
    {
        var clicked = false;
        var cut = RenderComponent<CoachMessagesCardComponent>(p => p
            .Add(x => x.Message, new CoachMessagePreviewModel())
            .Add(x => x.OnClick, () => { clicked = true; return Task.CompletedTask; }));

        cut.Find(".coach-messages-card").Click();
        clicked.Should().BeTrue();
    }
}
