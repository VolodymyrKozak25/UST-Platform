using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DAL.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {
        }
    }
}
