using GymCoach.Client.Client.Services;
using GymCoach.Client.Client.Services.Localization;
using GymCoach.Shared.Dtos;
using GymCoach.Shared.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Coach.CoachDashboard;

public partial class CoachDashboard : IDisposable
{
    [Inject] private ICoachService CoachService { get; set; } = default!;
    [Inject] private ILocalizationService L10n { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    protected bool IsLoading { get; private set; } = true;
    protected bool ApiUnavailable { get; private set; }
    protected CoachDashboardDto? Dashboard { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        L10n.CultureChanged += OnCultureChanged;
        await LoadDashboardAsync();
    }

    private async void OnCultureChanged() => await InvokeAsync(StateHasChanged);

    private async Task LoadDashboardAsync()
    {
        IsLoading = true;
        ApiUnavailable = false;

        try
        {
            var result = await CoachService.GetDashboardAsync();
            if (result?.IsSuccess == true)
            {
                Dashboard = result.Value;
            }
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

    private void NavigateAthletes(string? status)
    {
        var url = string.IsNullOrEmpty(status) ? "/athletes" : $"/athletes?status={status}";
        Navigation.NavigateTo(url);
    }

    private void NavigateAthlete(Guid athleteId) => Navigation.NavigateTo($"/athletes/{athleteId}");

    private string GetInitials(CoachAlertDto alert)
    {
        var name = GetAthleteName(alert);
        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2)
        {
            return $"{parts[0][0]}{parts[1][0]}".ToUpperInvariant();
        }

        return parts.Length > 0 && parts[0].Length > 0 ? parts[0][0].ToString().ToUpperInvariant() : "?";
    }

    private string GetAthleteName(CoachAlertDto alert) =>
        L10n.IsRtl ? alert.AthleteNameFa : alert.AthleteNameEn;

    private string GetAlertMessage(CoachAlertDto alert) =>
        L10n.IsRtl ? alert.MessageFa : alert.MessageEn;

    private string GetProgramAthleteName(ProgramExpiringDto program) =>
        L10n.IsRtl ? program.AthleteNameFa : program.AthleteNameEn;

    private string GetProgramName(ProgramExpiringDto program) =>
        L10n.IsRtl ? program.ProgramNameFa : program.ProgramNameEn;

    private static Color GetSeverityColor(CoachAlertSeverity severity) => severity switch
    {
        CoachAlertSeverity.Severe => Color.Error,
        CoachAlertSeverity.Warning => Color.Warning,
        _ => Color.Info
    };

    private string GetAlertTypeLabel(CoachAlertType type) => type switch
    {
        CoachAlertType.NoWorkout7Days => L10n["CoachDashboard.Alert.NoWorkout"],
        CoachAlertType.NeedsNewProgram => L10n["CoachDashboard.Alert.NeedsProgram"],
        CoachAlertType.ProgramExpiring => L10n["CoachDashboard.Alert.Expiring"],
        CoachAlertType.ProgramExpired => L10n["CoachDashboard.Alert.Expired"],
        _ => type.ToString()
    };

    private static double GetProgramProgress(ProgramExpiringDto program)
    {
        if (program.IsExpired)
        {
            return 100;
        }

        return Math.Clamp(100 - program.DaysRemaining * 10, 10, 100);
    }

    private string FormatDate(DateOnly date)
    {
        var text = date.ToString("MMM d", L10n.CurrentCulture);
        if (!L10n.IsRtl)
        {
            return text;
        }

        return text
            .Replace('0', '۰').Replace('1', '۱').Replace('2', '۲').Replace('3', '۳')
            .Replace('4', '۴').Replace('5', '۵').Replace('6', '۶').Replace('7', '۷')
            .Replace('8', '۸').Replace('9', '۹');
    }

    public void Dispose() => L10n.CultureChanged -= OnCultureChanged;
}
