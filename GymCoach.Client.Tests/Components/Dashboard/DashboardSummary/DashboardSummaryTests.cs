using FluentAssertions;
using GymCoach.Client.Client.Services;
using GymCoach.Client.Tests.Support;
using GymCoach.Shared.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using DashboardSummaryComponent = GymCoach.Client.Client.Components.Dashboard.DashboardSummary.DashboardSummary;

namespace GymCoach.Client.Tests.Components.Dashboard.DashboardSummaryTests;

public class DashboardSummaryTests : VoltComponentTestBase
{
    [Fact]
    public async Task Shows_Stats_When_Athletes_Exist()
    {
        var athletes = new Mock<IAthleteService>();
        athletes.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<AthleteDto> { new() { Id = Guid.NewGuid(), FullName = "Alex", Email = "a@x.com" } });

        var plans = new Mock<IWorkoutPlanService>();
        plans.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Array.Empty<WorkoutPlanDto>());

        Services.AddSingleton(athletes.Object);
        Services.AddSingleton(plans.Object);

        var cut = RenderComponent<DashboardSummaryComponent>();
        await Task.Delay(250);
        cut.Markup.Should().Contain("Athletes");
        cut.Markup.Should().Contain("1");
    }
}
