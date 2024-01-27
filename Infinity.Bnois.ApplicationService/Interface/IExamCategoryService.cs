using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IExamCategoryService
    {
        List<ExamCategoryModel> GetExamCategories(int pageSize, int pageNumber,string searchText, out int total);
        Task<ExamCategoryModel> GetExamCategory(int examCategoryId);
        Task<ExamCategoryModel> SaveExamCategory(int examCategoryId, ExamCategoryModel model);
        Task<bool> DeleteExamCategory(int examCategoryId);
        List<SelectModel> GetExamCategories();
        Task<List<SelectModel>> GetExamCategorySelectModels();
    }
}
