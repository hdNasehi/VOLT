using Bunit;
using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Xunit;
using PendingRequestsCardComponent = GymCoach.Client.Client.Components.Athlete.PendingRequestsCard.PendingRequestsCard;

namespace GymCoach.Client.Tests.Components.Athlete.PendingRequestsCard;

public class PendingRequestsCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Pending_Counts()
    {
        var requests = new PendingRequestsModel { PhotoRequests = 2, MeasurementRequests = 1 };

        var cut = RenderComponent<PendingRequestsCardComponent>(p => p.Add(x => x.Requests, requests));

        cut.Markup.Should().Contain("2");
        cut.Markup.Should().Contain("1");
    }

    [Fact]
    public void Invokes_OnPhotoRequestsClick()
    {
        var clicked = false;
        var cut = RenderComponent<PendingRequestsCardComponent>(p => p
            .Add(x => x.Requests, new PendingRequestsModel())
            .Add(x => x.OnPhotoRequestsClick, () => { clicked = true; return Task.CompletedTask; }));

        cut.FindAll(".request-row")[0].Click();
        clicked.Should().BeTrue();
    }
}
