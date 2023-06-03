using BLL.Services.IServices;
using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CourseService : CourseRepository, ICourseService
    {

        public CourseService(DbContext context) : base(context)
        {
        }

        //public bool CheckCourseKey(string courseKey)
        //{
        //    var course = FindByKey(courseKey);

        //    if (course != null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
