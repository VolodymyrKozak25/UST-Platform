using BLL.Services.IServices;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Services
{
    public class StartupService : IStartupService
    {
        public async Task CreateAdmins(IServiceProvider serviceProvider, List<(User, string)> admins)
        {
            using (var scope = serviceProvider.CreateScope()) // Create a scope
            {
                var scopedProvider = scope.ServiceProvider; // Get the scoped service provider
                var userManager = scopedProvider.GetRequiredService<UserManager<User>>();
                var adminCreatorService = new AdminCreatorService();

                await adminCreatorService.CreateAdminAsync(userManager, admins);
            }
        }

        public async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // initializing custom roles
            using (var scope = serviceProvider.CreateScope()) // Create a scope
            {
                var scopedProvider = scope.ServiceProvider; // Get the scoped service provider
                var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // initializing custom roles
                string[] roleNames = { "Admin", "Student", "Teacher" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName); // ensure that the role does not exist
                    if (!roleExist)
                    {
                        // create the roles and seed them to the database: 
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
        }
    }
}
