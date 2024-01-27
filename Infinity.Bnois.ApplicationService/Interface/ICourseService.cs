using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICourseService
    {
        List<CourseModel> GetCourses(int ps, int pn, string qs, out int total);
        Task<CourseModel> GetCourse(int id);
        Task<CourseModel> SaveCourse(int v, CourseModel model);
        Task<bool> DeleteCourse(int id);
        Task<List<SelectModel>> GetCourseSelectModels(int catId,int subCatId, int countryId);
        Task<List<SelectModel>> GetCourseBySubCategory(int subCatId);
        Task<List<SelectModel>> GetCourseByCategory(int catId);
        Task<List<SelectModel>> GetCourseSelectModels();
       


    }
}
