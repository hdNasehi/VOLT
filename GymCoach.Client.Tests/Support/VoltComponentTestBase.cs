using Bunit;
using GymCoach.Client.Client.Services.Localization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace GymCoach.Client.Tests.Support;

public abstract class VoltComponentTestBase : TestContext
{
    protected ILocalizationService L10n { get; }

    protected VoltComponentTestBase()
    {
        Services.AddMudServices();
        L10n = new LocalizationService();
        Services.AddSingleton<ILocalizationService>(L10n);
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
}
