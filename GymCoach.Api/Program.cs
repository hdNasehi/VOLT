using GymCoach.Api.Services;
using GymCoach.Database;
using GymCoach.Shared.Constants;
using GymCoach.Shared.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddGymCoachDatabase(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost\\SQLEXPRESS;Database=GymCoach;Trusted_Connection=True;TrustServerCertificate=True");

builder.Services.AddSingleton<ICurrentCoachProvider, ConfigCurrentCoachProvider>();
builder.Services.AddScoped<ICoachDashboardService, CoachDashboardService>();
builder.Services.AddScoped<IAthletePhoneService, AthletePhoneService>();
builder.Services.AddScoped<IAthleteRosterService, AthleteRosterService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Client", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Rider/VS may use 127.0.0.1, IIS Express ports, or alternate localhost ports.
            policy.SetIsOriginAllowed(static origin =>
            {
                if (string.IsNullOrEmpty(origin) || !Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                {
                    return false;
                }

                return uri.Host is "localhost" or "127.0.0.1";
            });
        }
        else
        {
            policy.WithOrigins(builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? ["https://localhost:7275", "http://localhost:5229"]);
        }

        policy.AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

await app.Services.MigrateAndSeedAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Client");
app.UseHttpsRedirection();
app.MapControllers();

app.MapGet(ApiRoutes.Health, () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.MapGet(ApiRoutes.Exercises, () => Results.Ok(Array.Empty<ExerciseDto>()));
app.MapGet(ApiRoutes.WorkoutPlans, () => Results.Ok(Array.Empty<WorkoutPlanDto>()));
app.MapGet(ApiRoutes.WorkoutTracking, () => Results.Ok(Array.Empty<WorkoutSessionDto>()));
app.MapGet(ApiRoutes.Measurements, () => Results.Ok(Array.Empty<MeasurementDto>()));
app.MapGet(ApiRoutes.AiCoach, () => Results.Ok(Array.Empty<AiCoachMessageDto>()));

app.Run();
