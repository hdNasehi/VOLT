using FluentAssertions;
using GymCoach.Client.Client.Models.Athlete;
using GymCoach.Client.Tests.Support;
using Xunit;
using MeasurementCardComponent = GymCoach.Client.Client.Components.Athlete.MeasurementCard.MeasurementCard;

namespace GymCoach.Client.Tests.Components.Athlete.MeasurementCard;

public class MeasurementCardTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Weight_And_BodyFat()
    {
        var progress = new BodyProgressModel { WeightKg = 80.5m, BodyFatPercent = 12.3m };

        var cut = RenderComponent<MeasurementCardComponent>(p => p.Add(x => x.Progress, progress));

        cut.Markup.Should().Contain("80.5");
        cut.Markup.Should().Contain("12.3");
    }
}
