﻿using MeetUp.Interfaces;
using MeetUp.Models;
using Microsoft.AspNetCore.Identity;


namespace MeetUp.Data
{
    public class Seed
    {

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "admin@gmail.com",
                        Email = adminUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@user.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user@gmail.com",
                        Email = appUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

        public static async Task SeedLocations(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var locationRepository = serviceScope.ServiceProvider.GetRequiredService<ILocationRepository>();
                locationRepository.Add(new Location(2.2m, 2.9m, "Split"));
                locationRepository.Add(new Location(1.0m, 0.3m, "Omiš"));
                locationRepository.Add(new Location(1.2m, 2.3m, "Makarska"));
            }
        }

        public static async Task SeedCategories(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var locationRepository = serviceScope.ServiceProvider.GetRequiredService<ICategoryRepository>();
                locationRepository.Add(new Category("Sport"));
                locationRepository.Add(new Category("Games"));
                locationRepository.Add(new Category("Music"));
            }
        }
    }
}