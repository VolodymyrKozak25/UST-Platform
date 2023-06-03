using DAL.Models;

namespace DAL.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        //Course? FindByKey(string courseKey);
    }
}
