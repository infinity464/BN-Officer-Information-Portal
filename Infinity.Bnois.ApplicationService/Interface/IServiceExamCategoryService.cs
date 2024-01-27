using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IServiceExamCategoryService
    {
        List<ServiceExamCategoryModel> GetServiceExamCategories(int pageSize, int pageNumber, string searchText, out int total);
        Task<ServiceExamCategoryModel> GetServiceExamCategory(int id);
        Task<ServiceExamCategoryModel> SaveServiceExamCategory(int id, ServiceExamCategoryModel model);
        Task<bool> DeleteServiceExamCategory(int id);
        Task<List<SelectModel>> GetServiceExamCategorySelectModels();
    }
}