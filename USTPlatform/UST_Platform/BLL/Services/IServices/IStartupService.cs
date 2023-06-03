using DAL.Models;

namespace BLL.Services.IServices
{
    public interface IStartupService
    {
        Task CreateRoles(IServiceProvider serviceProvider);

        Task CreateAdmins(IServiceProvider serviceProvider, List<(User, string)> admins);
    }
}
