using System.Globalization;

namespace GymCoach.Client.Client.Services.Localization;

public interface ILocalizationService
{
    CultureInfo CurrentCulture { get; }
    bool IsRtl { get; }
    string this[string key] { get; }
    event Action? CultureChanged;
    Task SetCultureAsync(string cultureName);
    string FormatNumber(decimal value, int decimals = 1);
}
