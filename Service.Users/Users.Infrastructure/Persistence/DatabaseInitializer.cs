using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Entities;

namespace Users.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole<Guid>("Admin");
                await roleManager.CreateAsync(role);
            }

            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = User.Create(
                    "admin@email.com",
                    "Admnin",
                    "Admin",
                    Guid.NewGuid()
                ).Value;

                var result = await userManager.CreateAsync(adminUser, "Admin12#");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
