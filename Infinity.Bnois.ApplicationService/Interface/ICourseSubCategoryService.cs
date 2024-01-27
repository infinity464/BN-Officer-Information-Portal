using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICourseSubCategoryService
    {
        List<CourseSubCategoryModel> GetCourseSubCategories(int ps, int pn, string qs, out int total);
        Task<CourseSubCategoryModel> GetCourseSubCategory(int id);
        Task<CourseSubCategoryModel> SaveCourseSubCategory(int v, CourseSubCategoryModel model);
        Task<bool> DeleteCourseSubCategory(int id);
        Task<List<SelectModel>> GetCourseSubCategorySelectModels(int id);
        Task<List<SelectModel>> GetCourseSubCategorySelectModels();
    }
}
