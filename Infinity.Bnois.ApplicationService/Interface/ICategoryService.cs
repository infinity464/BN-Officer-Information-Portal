using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICategoryService
    {
        List<CategoryModel> GetCategories(int ps, int pn, string qs, out int total);
        Task<CategoryModel> GetCategory(int id);
        Task<CategoryModel> SaveCategory(int v, CategoryModel model);
        Task<bool> DeleteCategoryAsync(int id);
        Task<List<SelectModel>> GetCategorySelectModels();
    }
}
