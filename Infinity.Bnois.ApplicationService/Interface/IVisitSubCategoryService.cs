using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IVisitSubCategoryService
    {
        List<VisitSubCategoryModel> GetVisitSubCategories(int ps, int pn, string qs, out int total);
        Task<VisitSubCategoryModel> GetVisitSubCategory(int id);
        Task<VisitSubCategoryModel> SaveVisitSubCategory(int v, VisitSubCategoryModel model);
        Task<bool> DeleteVisitSubCategory(int id);
        Task<List<SelectModel>> GetVisitSubCategorySelectModelsByVisitCategory(int id);
        Task<List<SelectModel>> GetVisitSubCategorySelectModels();
  
    }
}