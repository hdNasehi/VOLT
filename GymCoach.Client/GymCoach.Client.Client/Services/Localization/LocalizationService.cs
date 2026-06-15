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
        ["Splash.Tagline"] = ("Gym Management & AI Coach", "مدیریت باشگاه و مربی هوشمند"),
        ["Login.Title"] = ("Sign in to VOLT", "ورود به ولت"),
        ["Login.Subtitle"] = ("Train smarter with your coach", "هوشمندتر با مربی خود تمرین کنید"),
        ["Login.Phone"] = ("Mobile", "موبایل"),
        ["Login.Password"] = ("Password", "رمز عبور"),
        ["Login.Submit"] = ("Sign in", "ورود"),
        ["Login.Error"] = ("Invalid phone number or password.", "شماره موبایل یا رمز عبور نادرست است."),
        ["Login.TestHint"] = ("Test athlete: 09120000000 / Test123!", "ورزشکار تست: 09120000000 / Test123!"),
        ["Athlete.Welcome"] = ("Welcome", "سلام"),
        ["Athlete.Coach"] = ("Coach", "مربی"),
        ["Athlete.Gym"] = ("Gym", "باشگاه"),
        ["Athlete.ActiveProgram"] = ("Active Program", "برنامه فعال"),
        ["Athlete.Day"] = ("Day", "روز"),
        ["Athlete.DaysRemaining"] = ("Days left", "روز مانده"),
        ["Athlete.Progress"] = ("Progress", "پیشرفت"),
        ["Athlete.StartWorkout"] = ("Start Workout", "شروع تمرین"),
        ["Athlete.BodyProgress"] = ("Body Progress", "پیشرفت بدنی"),
        ["Athlete.Weight"] = ("Weight", "وزن"),
        ["Athlete.BodyFat"] = ("Body Fat", "چربی بدن"),
        ["Athlete.LastMeasured"] = ("Last measured", "آخرین اندازه‌گیری"),
        ["Athlete.CoachMessages"] = ("Coach Messages", "پیام‌های مربی"),
        ["Athlete.Unread"] = ("unread", "خوانده‌نشده"),
        ["Athlete.PendingRequests"] = ("Pending Requests", "درخواست‌های در انتظار"),
        ["Athlete.PhotoRequests"] = ("Photo Requests", "درخواست عکس"),
        ["Athlete.MeasurementRequests"] = ("Measurement Requests", "درخواست اندازه‌گیری"),
        ["Athlete.Pending"] = ("pending", "در انتظار"),
        ["Athlete.WeightTrend"] = ("Weight Trend", "روند وزن"),
        ["Athlete.BodyFatTrend"] = ("Body Fat Trend", "روند چربی"),
        ["Athlete.PreviousPrograms"] = ("Previous Programs", "برنامه‌های قبلی"),
        ["Athlete.Active"] = ("Active", "فعال"),
        ["Athlete.Completed"] = ("Completed", "تکمیل‌شده"),
        ["Athlete.Nav.Programs"] = ("Programs", "برنامه‌ها"),
        ["Athlete.Nav.Photos"] = ("Photos", "عکس‌ها"),
        ["Athlete.Dashboard.Loading"] = ("Loading dashboard...", "در حال بارگذاری داشبورد..."),
        ["Athlete.Programs.Title"] = ("My Programs", "برنامه‌های من"),
        ["Athlete.Programs.Subtitle"] = ("Active and completed training blocks", "بلوک‌های تمرینی فعال و تکمیل‌شده"),
        ["Athlete.Programs.Active"] = ("Active Programs", "برنامه‌های فعال"),
        ["Athlete.Programs.Completed"] = ("Completed Programs", "برنامه‌های تکمیل‌شده"),
        ["Athlete.Programs.NoneCompleted"] = ("No completed programs yet", "هنوز برنامه تکمیل‌شده‌ای ندارید"),
        ["Athlete.Program.NotFound"] = ("Program not found", "برنامه یافت نشد"),
        ["Athlete.Program.Details"] = ("Program details", "جزئیات برنامه"),
        ["Athlete.Program.Goal"] = ("Goal", "هدف"),
        ["Athlete.Program.Duration"] = ("Duration", "مدت"),
        ["Athlete.Program.Weeks"] = ("weeks", "هفته"),
        ["Athlete.Program.Days"] = ("Training days", "روزهای تمرین"),
        ["Athlete.Day.NotFound"] = ("Workout day not found", "روز تمرین یافت نشد"),
        ["Athlete.Exercise.Tips"] = ("Tips", "نکات"),
        ["Athlete.Exercise.Mistakes"] = ("Common mistakes", "اشتباهات رایج"),
        ["Athlete.Exercise.Rest"] = ("Rest", "استراحت"),
        ["Athlete.Workout.NoSession"] = ("No active workout session", "جلسه تمرین فعالی وجود ندارد"),
        ["Athlete.BackDashboard"] = ("Back to dashboard", "بازگشت به داشبورد"),
        ["Athlete.Workout.InProgress"] = ("Workout in progress", "تمرین در جریان"),
        ["Athlete.Workout.SessionTime"] = ("Session time", "زمان جلسه"),
        ["Athlete.Workout.Rest"] = ("Rest", "استراحت"),
        ["Athlete.Workout.RestExceeded"] = ("Rest exceeded", "استراحت بیش از حد"),
        ["Athlete.Workout.SkipRest"] = ("Skip rest", "رد کردن استراحت"),
        ["Athlete.Workout.StartExercise"] = ("Start", "شروع"),
        ["Athlete.Workout.CompleteExercise"] = ("Complete", "تکمیل"),
        ["Athlete.Workout.Completed"] = ("Done", "انجام شد"),
        ["Athlete.Workout.Locked"] = ("Complete previous exercise first", "ابتدا حرکت قبلی را تکمیل کنید"),
        ["Athlete.Photos.Title"] = ("Photo Requests", "درخواست‌های عکس"),
        ["Athlete.Photos.Subtitle"] = ("Upload progress photos for your coach", "عکس‌های پیشرفت را برای مربی آپلود کنید"),
        ["Athlete.Photos.Upload"] = ("Upload photo", "آپلود عکس"),
        ["Athlete.Photos.Pending"] = ("Pending review", "در انتظار بررسی"),
        ["Athlete.Photos.Approved"] = ("Approved", "تأیید شده"),
        ["Athlete.Photos.Rejected"] = ("Rejected", "رد شده"),
        ["Goal.Hypertrophy"] = ("Hypertrophy", "حجم‌گیری"),
        ["Goal.Strength"] = ("Strength", "قدرت"),
        ["Goal.FatLoss"] = ("Fat loss", "چربی‌سوزی"),
        ["Goal.Recomp"] = ("Recomposition", "ترکیب‌بندی"),
        ["Admin.Title"] = ("Admin Dashboard", "داشبورد مدیر سیستم"),
        ["Admin.Subtitle"] = ("Platform administration", "مدیریت پلتفرم"),
        ["Admin.ComingSoon"] = ("Admin tools coming soon", "ابزارهای مدیریت به‌زودی"),
        ["Gym.Title"] = ("Gym Dashboard", "داشبورد باشگاه"),
        ["Gym.Subtitle"] = ("Gym management", "مدیریت باشگاه"),
        ["Gym.ComingSoon"] = ("Gym tools coming soon", "ابزارهای باشگاه به‌زودی")
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
