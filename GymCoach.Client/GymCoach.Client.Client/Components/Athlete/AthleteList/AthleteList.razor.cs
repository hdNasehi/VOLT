using GymCoach.Client.Client.Services;
using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace GymCoach.Client.Client.Components.Athlete.AthleteList;

public partial class AthleteList
{
    [Inject] private IAthleteService AthleteService { get; set; } = default!;

    [Parameter] public EventCallback<Guid> OnSelected { get; set; }

    private bool _isLoading = true;
    private List<AthleteDto> _athletes = [];
    private List<AthleteDto> _filtered = [];
    private string _filter = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        _athletes = (await AthleteService.GetAllAsync()).ToList();
        _filtered = _athletes;
        _isLoading = false;
    }

    private async Task FilterAthletes(string term)
    {
        _filter = term;
        _filtered = string.IsNullOrWhiteSpace(_filter)
            ? _athletes
            : _athletes.Where(a =>
                a.FullName.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                a.Email.Contains(_filter, StringComparison.OrdinalIgnoreCase)).ToList();
        await Task.CompletedTask;
    }

    private async Task SelectAthlete(Guid id)
    {
        if (OnSelected.HasDelegate)
        {
            await OnSelected.InvokeAsync(id);
        }
    }

    private static string GetInitials(string name)
    {
        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length >= 2
            ? $"{parts[0][0]}{parts[^1][0]}".ToUpperInvariant()
            : name.Length > 0 ? name[0].ToString().ToUpperInvariant() : "?";
    }
}
