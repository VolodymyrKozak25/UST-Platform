using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(DbContext context) : base(context)
        {
        }
    }
}
