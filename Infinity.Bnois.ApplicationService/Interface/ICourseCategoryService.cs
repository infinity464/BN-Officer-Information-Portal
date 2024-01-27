using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICourseCategoryService
    {
        List<CourseCategoryModel> GetCourseCategories(int ps, int pn, string qs, out int total);
        Task<CourseCategoryModel> GetCourseCategory(int id);
        Task<CourseCategoryModel> SaveCourseCategory(int v, CourseCategoryModel model);
        Task<bool> DeleteCourseCategory(int id);
        Task<List<SelectModel>> GetCourseCategorySelectModels();
        Task<List<SelectModel>> GetCourseCategorySelectModelsByTrace();
    }
}