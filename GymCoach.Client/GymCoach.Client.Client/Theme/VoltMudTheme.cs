using MudBlazor;

namespace GymCoach.Client.Client.Theme;

public static class VoltMudTheme
{
    public static MudTheme Create() => new()
    {
        PaletteDark = new PaletteDark
        {
            Primary = "#CBFB54",
            Secondary = "#7C4DFF",
            Tertiary = "#9A6CFF",
            Background = "#0A0B0D",
            Surface = "#16181D",
            AppbarBackground = "#0E1013",
            DrawerBackground = "#16181D",
            TextPrimary = "#F4F6F8",
            TextSecondary = "#99A0AB",
            TextDisabled = "#5C636E",
            Success = "#35D9A0",
            Warning = "#FFC24B",
            Error = "#FF5C49",
            Info = "#4FD2E0",
            ActionDefault = "#99A0AB",
            Divider = "rgba(255,255,255,0.075)",
            LinesDefault = "rgba(255,255,255,0.075)"
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "16px"
        }
    };
}
