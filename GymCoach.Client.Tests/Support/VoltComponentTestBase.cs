using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace GymCoach.Client.Tests.Support;

public abstract class VoltComponentTestBase : TestContext
{
    protected VoltComponentTestBase()
    {
        Services.AddMudServices();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
}
