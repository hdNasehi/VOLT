using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Athlete.AthleteDashboardHeader;

public partial class AthleteDashboardHeader
{
    [Parameter] public string AthleteName { get; set; } = string.Empty;
    [Parameter] public string CoachName { get; set; } = string.Empty;
    [Parameter] public string GymName { get; set; } = string.Empty;
    [Parameter] public string WelcomeLabel { get; set; } = "Welcome";
    [Parameter] public string CoachLabel { get; set; } = "Coach";
    [Parameter] public string GymLabel { get; set; } = "Gym";

    private static string GetInitials(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "?";
        }

        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length >= 2
            ? $"{parts[0][0]}{parts[^1][0]}"
            : name[..Math.Min(2, name.Length)];
    }
}
