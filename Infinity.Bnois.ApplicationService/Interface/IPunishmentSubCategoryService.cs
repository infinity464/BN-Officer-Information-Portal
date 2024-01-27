using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPunishmentSubCategoryService
    {
        List<PunishmentSubCategoryModel> GetPunishmentSubCategories(int ps, int pn, string qs, out int total);
        Task<PunishmentSubCategoryModel> GetPunishmentSubCategory(int id);
        Task<PunishmentSubCategoryModel> SavePunishmentSubCategory(int v, PunishmentSubCategoryModel model);
        Task<bool> DeletePunishmentSubCategory(int id);
        Task<List<SelectModel>> GetPunishmentSubCategorySelectModelsByPunishmentCategory(int id);
        Task<List<SelectModel>> GetPunishmentSubCategoryForTrace();
        Task<List<SelectModel>> GetPunishmentSubCategorySelectModels();
    }
}