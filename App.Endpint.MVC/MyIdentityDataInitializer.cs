using App.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.Endpoint.MVC;

public static class MyIdentityDataInitializer
{
    public static async Task SeedData
    (UserManager<IdentityUser<int>> userManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        await SeedRoles(roleManager);
        await SeedUsers(userManager);
    }

    private static async Task SeedUsers(UserManager<IdentityUser<int>> userManager)
    {
        if (await userManager.FindByNameAsync("Admin@a.a") == null)
        {
            var user = new IdentityUser<int>(userName: "Admin@a.a");
            user.LockoutEnabled = false;

            var result = await userManager.CreateAsync
                (user, "Admin123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user,
                    "Admin");
            }
        }

    }

    private static async Task SeedRoles
            (RoleManager<IdentityRole<int>> roleManager)
    {
        if (!await roleManager.RoleExistsAsync
                ("Admin"))
        {
            var role = new IdentityRole<int>(roleName: "Admin");
            var roleResult = await roleManager.
                CreateAsync(role);
        }


        if (!await roleManager.RoleExistsAsync
                ("Costumer"))
        {
            var role = new IdentityRole<int>("Costumer");
            var roleResult = await roleManager.
                CreateAsync(role);
        }

        if (!await roleManager.RoleExistsAsync
                ("Worker"))
        {
            var role = new IdentityRole<int>("Worker");
            var roleResult = await roleManager.
                CreateAsync(role);
        }
    }
}