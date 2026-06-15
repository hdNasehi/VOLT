using Bunit;
using FluentAssertions;
using GymCoach.Client.Tests.Support;
using GymCoach.Shared.Dtos;
using Xunit;
using LoginFormComponent = GymCoach.Client.Client.Components.Auth.LoginForm.LoginForm;

namespace GymCoach.Client.Tests.Components.Auth.LoginForm;

public class LoginFormTests : VoltComponentTestBase
{
    [Fact]
    public void Renders_Title_And_Fields()
    {
        var cut = RenderComponent<LoginFormComponent>(p => p
            .Add(x => x.Title, "Sign in")
            .Add(x => x.PhoneLabel, "Phone")
            .Add(x => x.PasswordLabel, "Password"));

        cut.Markup.Should().Contain("Sign in");
        cut.Markup.Should().Contain("Phone");
        cut.Markup.Should().Contain("Password");
    }

    [Fact]
    public void Shows_Error_Message()
    {
        var cut = RenderComponent<LoginFormComponent>(p => p.Add(x => x.ErrorMessage, "Invalid login"));

        cut.Markup.Should().Contain("Invalid login");
    }
}
