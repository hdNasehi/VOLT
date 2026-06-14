using GymCoach.Api.Extensions;
using GymCoach.Api.Services;
using GymCoach.Database;
using GymCoach.Database.Identity;
using GymCoach.Shared.Constants;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddVoltAuthentication(builder.Configuration);
builder.Services.AddGymCoachDatabase(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost\\SQLEXPRESS;Database=GymCoach;Trusted_Connection=True;TrustServerCertificate=True");

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.User.RequireUniqueEmail = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GymCoachDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<ICurrentCoachProvider, ConfigCurrentCoachProvider>();
builder.Services.AddScoped<ICurrentUserContext, HttpCurrentUserContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICoachDashboardService, CoachDashboardService>();
builder.Services.AddScoped<ICoachManagementService, CoachManagementService>();
builder.Services.AddScoped<IAthletePhoneService, AthletePhoneService>();
builder.Services.AddScoped<IAthleteRosterService, AthleteRosterService>();
builder.Services.AddScoped<IAthleteRequestService, AthleteRequestService>();
builder.Services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProgressService, ProgressService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Client", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet(ApiRoutes.Health, () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.Run();
