using System.Globalization;

namespace GymCoach.Client.Client.Services.Localization;

public sealed class LocalizationService : ILocalizationService
{
    private static readonly Dictionary<string, (string En, string Fa)> Strings = new()
    {
        ["AppName"] = ("VOLT", "ولت"),
        ["Nav.Home"] = ("Home", "خانه"),
        ["Nav.Library"] = ("Library", "کتابخانه"),
        ["Nav.AiCoach"] = ("AI Coach", "مربی هوشمند"),
        ["Nav.Progress"] = ("Progress", "پیشرفت"),
        ["Nav.Profile"] = ("Profile", "پروفایل"),
        ["Lang.En"] = ("EN", "EN"),
        ["Lang.Fa"] = ("فا", "فا"),
        ["Dashboard.Title"] = ("Dashboard", "داشبورد"),
        ["Dashboard.Subtitle"] = ("Your training overview", "نمای کلی تمرین شما"),
        ["CoachDashboard.Title"] = ("Coach Dashboard", "داشبورد مربی"),
        ["CoachDashboard.Subtitle"] = ("Who needs your attention today", "امروز به چه کسانی نیاز دارید"),
        ["CoachDashboard.Loading"] = ("Loading dashboard...", "در حال بارگذاری داشبورد..."),
        ["CoachDashboard.ApiUnavailable"] = ("API not reachable", "API در دسترس نیست"),
        ["CoachDashboard.ApiUnavailableHint"] = ("Start GymCoach.Api (https://localhost:7219) or use the Api + Client launch profile.", "GymCoach.Api را اجرا کنید یا از پروفایل Api + Client استفاده کنید."),
        ["CoachDashboard.Empty"] = ("No athletes yet", "هنوز ورزشکاری ندارید"),
        ["CoachDashboard.EmptyHint"] = ("Add athletes by phone to get started.", "با شماره موبایل ورزشکار اضافه کنید."),
        ["CoachDashboard.Kpi.Total"] = ("Total", "کل"),
        ["CoachDashboard.Kpi.Active"] = ("Active", "فعال"),
        ["CoachDashboard.Kpi.Inactive"] = ("Inactive", "غیرفعال"),
        ["CoachDashboard.QuickActions"] = ("Quick actions", "اقدامات سریع"),
        ["CoachDashboard.AddAthlete"] = ("Add athlete", "افزودن ورزشکار"),
        ["CoachDashboard.NewProgram"] = ("New program", "برنامه جدید"),
        ["CoachDashboard.LogMeasurement"] = ("Log measurement", "ثبت اندازه‌گیری"),
        ["CoachDashboard.NeedsProgram"] = ("Needs program", "نیاز به برنامه"),
        ["CoachDashboard.NoWorkout"] = ("No workout 7+ days", "بدون تمرین ۷+ روز"),
        ["CoachDashboard.Attention"] = ("Needs your attention", "نیاز به توجه شما"),
        ["CoachDashboard.ExpiringPrograms"] = ("Programs expiring", "برنامه‌های در حال انقضا"),
        ["CoachDashboard.RecentMeasurements"] = ("Recent measurements", "اندازه‌گیری‌های اخیر"),
        ["CoachDashboard.RecentPhotos"] = ("Recent progress photos", "عکس‌های پیشرفت اخیر"),
        ["CoachDashboard.Expired"] = ("Expired", "منقضی"),
        ["CoachDashboard.DaysLeft"] = ("days left", "روز مانده"),
        ["CoachDashboard.Alert.NoWorkout"] = ("No workout", "بدون تمرین"),
        ["CoachDashboard.Alert.NeedsProgram"] = ("Needs program", "نیاز به برنامه"),
        ["CoachDashboard.Alert.Expiring"] = ("Expiring", "در حال انقضا"),
        ["CoachDashboard.Alert.Expired"] = ("Expired", "منقضی"),
        ["Splash.Tagline"] = ("Gym Management & AI Coach", "مدیریت باشگاه و مربی هوشمند")
    };

    private CultureInfo _culture = new("en");

    public CultureInfo CurrentCulture => _culture;
    public bool IsRtl => _culture.TwoLetterISOLanguageName == "fa";
    public event Action? CultureChanged;

    public string this[string key] => Strings.TryGetValue(key, out var pair)
        ? IsRtl ? pair.Fa : pair.En
        : key;

    public async Task SetCultureAsync(string cultureName)
    {
        _culture = CultureInfo.GetCultureInfo(cultureName);
        CultureInfo.DefaultThreadCurrentCulture = _culture;
        CultureInfo.DefaultThreadCurrentUICulture = _culture;
        CultureChanged?.Invoke();
        await Task.CompletedTask;
    }

    public string FormatNumber(decimal value, int decimals = 1)
    {
        if (!IsRtl)
        {
            return value.ToString($"F{decimals}", CultureInfo.InvariantCulture);
        }

        var formatted = value.ToString($"F{decimals}", CultureInfo.InvariantCulture);
        return formatted
            .Replace('0', '۰').Replace('1', '۱').Replace('2', '۲').Replace('3', '۳')
            .Replace('4', '۴').Replace('5', '۵').Replace('6', '۶').Replace('7', '۷')
            .Replace('8', '۸').Replace('9', '۹').Replace('.', '٫');
    }
}
