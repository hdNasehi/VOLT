using GymCoach.Shared.Constants;

namespace GymCoach.Database.Seed;

public static class RolesSeeder
{
    public static async Task SeedAsync(
        Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager,
        CancellationToken cancellationToken = default)
    {
        foreach (var role in Permissions.AllRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Microsoft.AspNetCore.Identity.IdentityRole(role));
            }
        }
    }
}
