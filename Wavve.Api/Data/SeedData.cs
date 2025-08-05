using Microsoft.AspNetCore.Identity;
using Wavve.Core.Identity;

namespace Wavve.Api.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string[] roleNames = { "User", "Admin" };
            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            var usersSection = configuration.GetSection("SeedUsers");
            var seedUsers = usersSection.Get<List<SeedUser>>() ?? new();

            foreach (var seedUser in seedUsers)
            {
                var existingUser = await userManager.FindByEmailAsync(seedUser.Email);
                if (existingUser != null) continue;

                var user = new ApplicationUser
                {
                    UserName = seedUser.UserName,
                    
                    Email = seedUser.Email,
                };

                var result = await userManager.CreateAsync(user, seedUser.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, seedUser.Role);
                }
            }
        }

        private class SeedUser
        {
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
    }
}
