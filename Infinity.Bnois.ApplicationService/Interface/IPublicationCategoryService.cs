using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPublicationCategoryService
    {
        List<PublicationCategoryModel> GetPublicationCategories(int pageSize, int pageNumber, string searchText, out int total);
        Task<PublicationCategoryModel> GetPublicationCategory(int id);
        Task<PublicationCategoryModel> SavePublicationCategory(int id, PublicationCategoryModel model);
        Task<bool> DeletePublicationCategory(int id);
        Task<List<SelectModel>> GetPublicationCategorySelectModels();
        Task<List<SelectModel>> GetTracePublicationCategorySelectModels();
    }
}