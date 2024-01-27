using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPunishmentCategoryService
    {
        List<PunishmentCategoryModel> GetPunishmentCategories(int pageSize, int pageNumber, string searchText, out int total);
        Task<PunishmentCategoryModel> GetPunishmentCategory(int id);
        Task<PunishmentCategoryModel> SavePunishmentCategory(int id, PunishmentCategoryModel model);
        Task<bool> DeletePunishmentCategory(int id);
        Task<List<SelectModel>> GetPunishmentCategorySelectModels();
    }
}