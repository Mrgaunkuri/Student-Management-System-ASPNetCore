using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Student_management_system.model
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider, string adminEmail = null, string adminPassword = null)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger("DbInitializer");

            string[] roleNames = { "Admin", "Teacher", "Student" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var createRoleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!createRoleResult.Succeeded)
                    {
                        logger?.LogWarning("Failed to create role {Role}. Errors: {Errors}", roleName, string.Join(", ", createRoleResult.Errors.Select(e => e.Description)));
                    }
                }
            }

            // Allow callers or configuration to override seeded admin credentials
            adminEmail ??= configuration?.GetValue<string>("Seed:AdminEmail") ?? "admin@example.com";
            adminPassword ??= configuration?.GetValue<string>("Seed:AdminPassword") ?? "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createAdminUser = await userManager.CreateAsync(newAdminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    var addToRoleResult = await userManager.AddToRoleAsync(newAdminUser, "Admin");
                    if (!addToRoleResult.Succeeded)
                    {
                        logger?.LogWarning("Failed to add seeded admin to role Admin. Errors: {Errors}", string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
                    }
                }
                else
                {
                    logger?.LogError("Failed to create admin user {Email}. Errors: {Errors}", adminEmail, string.Join(", ", createAdminUser.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                // Ensure existing admin user has the Admin role
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    var addRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (!addRoleResult.Succeeded)
                    {
                        logger?.LogWarning("Failed to add existing user {Email} to role Admin. Errors: {Errors}", adminEmail, string.Join(", ", addRoleResult.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
