using GymCoach.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace GymCoach.Client.Client.Components.Auth.LoginForm;

public partial class LoginForm
{
    private readonly AuthLoginRequest _model = new();
    private InputType _passwordInput = InputType.Password;
    private string _passwordIcon = Icons.Material.Filled.VisibilityOff;

    [Parameter] public string Title { get; set; } = "Sign in";
    [Parameter] public string? Subtitle { get; set; }
    [Parameter] public string PhoneLabel { get; set; } = "Mobile";
    [Parameter] public string PasswordLabel { get; set; } = "Password";
    [Parameter] public string SubmitLabel { get; set; } = "Sign in";
    [Parameter] public string? Hint { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }
    [Parameter] public bool IsBusy { get; set; }
    [Parameter] public EventCallback<AuthLoginRequest> OnSubmit { get; set; }

    private async Task HandleSubmitAsync() => await OnSubmit.InvokeAsync(_model);

    private Task HandleSubmitClick() => HandleSubmitAsync();

    private void TogglePassword()
    {
        if (_passwordInput == InputType.Password)
        {
            _passwordInput = InputType.Text;
            _passwordIcon = Icons.Material.Filled.Visibility;
        }
        else
        {
            _passwordInput = InputType.Password;
            _passwordIcon = Icons.Material.Filled.VisibilityOff;
        }
    }
}
