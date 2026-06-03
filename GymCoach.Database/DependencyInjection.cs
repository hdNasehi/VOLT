using GymCoach.Database.Repositories;
using GymCoach.Database.Seed;
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
        return services;
    }

    public static async Task MigrateAndSeedAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GymCoachDbContext>();
        await db.Database.MigrateAsync(cancellationToken);
        await DatabaseSeeder.SeedAsync(db, cancellationToken);
    }
}
