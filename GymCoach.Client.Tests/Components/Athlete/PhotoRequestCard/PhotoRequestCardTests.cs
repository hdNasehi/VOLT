using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using GymCoach.Shared.Enums;
using Xunit;
using PhotoRequestCardComponent = GymCoach.Client.Client.Components.Athlete.PhotoRequestCard.PhotoRequestCard;

namespace GymCoach.Client.Tests.Components.Athlete.PhotoRequestCard;

public class PhotoRequestCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Rejection_Reason()
    {
        var request = new PhotoRequestModel
        {
            BodyPart = "Arm",
            Instructions = "Flex",
            Status = AssessmentReviewStatus.Rejected,
            RejectionReason = "Too dark"
        };

        var cut = RenderComponent<PhotoRequestCardComponent>(p => p.Add(x => x.Request, request));

        cut.Markup.Should().Contain("Arm");
        cut.Markup.Should().Contain("Too dark");
    }
}
