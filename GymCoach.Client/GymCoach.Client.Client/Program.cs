using ApexCharts;
using GymCoach.Client.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddApexCharts();
builder.Services.AddMudServices();
builder.Services.AddGymCoachClientServices(builder.Configuration);

await builder.Build().RunAsync();
