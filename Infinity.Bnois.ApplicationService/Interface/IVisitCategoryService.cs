using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IVisitCategoryService
    {
        List<VisitCategoryModel> GetVisitCategories(int pageSize, int pageNumber, string searchText, out int total);
        Task<VisitCategoryModel> GetVisitCategory(int id);
        Task<VisitCategoryModel> SaveVisitCategory(int id, VisitCategoryModel model);
        Task<bool> DeleteVisitCategory(int id);
        Task<List<SelectModel>> GetVisitCategorySelectModels();
    }
}