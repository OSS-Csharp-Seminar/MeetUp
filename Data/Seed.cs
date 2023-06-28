using MeetUp.Interfaces;
using MeetUp.Models;
using MeetUp.Repositories;
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

        public static async Task SeedCities(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var cityRepository = serviceScope.ServiceProvider.GetRequiredService<ICityRepository>();
                cityRepository.Add(new City("Split"));
                cityRepository.Add(new City("Omis"));
                cityRepository.Add(new City("Makarska"));
            }
        }

        public static async Task SeedLocations(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var locationRepository = serviceScope.ServiceProvider.GetRequiredService<ILocationRepository>();
                locationRepository.Add(new Location("Ulica 1", 1));
                locationRepository.Add(new Location("Ulica 2", 2));
                locationRepository.Add(new Location("Ulica 3", 3));
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

        public static async Task SeedMeetActivities(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var activityRepository = serviceScope.ServiceProvider.GetRequiredService<IMeetActivityRepository>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                
                var titles = new string[]
                    { "Nogomet 4v4", "Online gaming", "Ide li ko vani?", "CarShare Zagreb", "Tražimo bassista" };

                var description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

                for (int i = 0; i < 5; i++)
                {
                    var picture = File.ReadAllBytes("wwwroot/SeedImages/0" + (i+1) + ".jpg");
                    activityRepository.Add(new MeetActivity(titles[i], description,
                        DateTime.Now, 5, picture, (i%3)+1, (i%3)+1, userManager.FindByEmailAsync("user@user.com").Result.Id));
                }
            }
        }
    }
}