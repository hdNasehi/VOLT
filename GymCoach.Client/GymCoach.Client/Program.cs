using GymCoach.Client.Client.Extensions;
using GymCoach.Client.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Shared with WASM client — required for prerender / static pass on the host.
builder.Services.AddMudServices();
builder.Services.AddGymCoachClientServices(builder.Configuration);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GymCoach.Client.Client._Imports).Assembly);

app.Run();
