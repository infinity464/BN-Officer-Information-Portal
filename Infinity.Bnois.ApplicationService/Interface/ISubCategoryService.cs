using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISubCategoryService
    {
        List<SubCategoryModel> GetSubCategories(int ps, int pn, string qs, out int total);
        Task<SubCategoryModel> GetSubCategory(int id);
        Task<SubCategoryModel> SaveSubCategory(int v, SubCategoryModel model);
        Task<bool> DeleteSubCategory(int id);
        Task<List<SelectModel>> GetSubCategorySelectModels();

        Task<List<SelectModel>> GetSubCategorySelectModelsByCategory(int categoryId);
        
    }
}
