using GymCoach.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymCoach.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddGymCoachDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GymCoachDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IAthleteRepository, AthleteRepository>();
        services.AddScoped<ICoachRepository, CoachRepository>();
        services.AddScoped<IProgressRepository, ProgressRepository>();

        return services;
    }

    public static async Task MigrateAndSeedAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GymCoachDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
        await db.Database.MigrateAsync(cancellationToken);
        await Seed.RolesSeeder.SeedAsync(roleManager, cancellationToken);
        await Seed.DatabaseSeeder.SeedAsync(db, cancellationToken);
    }
}
