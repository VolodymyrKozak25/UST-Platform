using BLL.Services.IServices;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AdminCreatorService : IAdminCreatorService
    {
        // Single strings
        public async Task CreateAdminAsync(UserManager<User> userManager, string firstName, string lastName, string middleName, string password, string email)
        {
            var admin = new User
            {
                UserName = $"{lastName}_{firstName}_{middleName}",
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Email = email,
            };

            var user = await userManager.FindByEmailAsync(admin.Email!);

            if (user == null)
            {
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, "Admin");

                var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, confirmationToken);
            }
        }

        // Single user
        public async Task CreateAdminAsync(UserManager<User> userManager, User admin, string password)
        {
            var user = await userManager.FindByEmailAsync(admin.Email!);
            if (user == null)
            {
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, "Admin");

                var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, confirmationToken);
            }
        }

        // List of admins
        public async Task CreateAdminAsync(UserManager<User> userManager, List<(User, string)> admins)
        {
            foreach (var admin in admins)
            {
                var user = await userManager.FindByEmailAsync(admin.Item1.Email!);
                if (user == null)
                {
                    await userManager.CreateAsync(admin.Item1, admin.Item2);
                    await userManager.AddToRoleAsync(admin.Item1, "Admin");

                    var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(admin.Item1);
                    await userManager.ConfirmEmailAsync(admin.Item1, confirmationToken);
                }
            }
        }
    }
}
