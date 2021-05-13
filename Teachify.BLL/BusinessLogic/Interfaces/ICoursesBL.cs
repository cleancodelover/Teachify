using System.Collections.Generic;
using System.Threading.Tasks;
using Teachify.DAL.DVM;

namespace Teachify.BLL.BusinessLogic.Interfaces
{
    public interface ICoursesBL
    {
        Task<int> AddCourse(CourseDVM dvm);
        Task<bool> DeleteCourse(int Id);
        Task<CourseDVM> GetCourseByCourseId(int Id);
        Task<List<CourseDVM>> GetCourses();
        Task<bool> CourseExists(int Id);
        Task<bool> UpdateCourse(CourseDVM dvm);
    }
}
