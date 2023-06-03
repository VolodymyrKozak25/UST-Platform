using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services.IServices
{
    public interface IAdminCreatorService
    {
        Task CreateAdminAsync(UserManager<User> userManager, string firstName, string lastName, string middleName, string password, string email);

        Task CreateAdminAsync(UserManager<User> userManager, User admin, string password);

        Task CreateAdminAsync(UserManager<User> userManager, List<(User, string)> admins);
    }
}
