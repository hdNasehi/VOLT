using GymCoach.Client.Client.Services;
using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Dashboard.DashboardSummary;

public partial class DashboardSummary
{
    [Inject] private IAthleteService AthleteService { get; set; } = default!;
    [Inject] private IWorkoutPlanService WorkoutPlanService { get; set; } = default!;

    protected bool IsLoading { get; private set; } = true;
    protected bool ApiUnavailable { get; private set; }
    protected int AthleteCount { get; private set; }
    protected int ActivePlanCount { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var athletes = await AthleteService.GetAllAsync();
            var plans = await WorkoutPlanService.GetAllAsync();
            AthleteCount = athletes.Count;
            ActivePlanCount = plans.Count;
        }
        catch (HttpRequestException)
        {
            ApiUnavailable = true;
        }
        catch (TaskCanceledException)
        {
            ApiUnavailable = true;
        }
        finally
        {
            IsLoading = false;
        }
    }
}
