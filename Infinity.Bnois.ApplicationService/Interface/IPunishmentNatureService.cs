using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPunishmentNatureService
    {
        List<PunishmentNatureModel> GetPunishmentNatures(int pageSize, int pageNumber, string searchText, out int total);
        Task<PunishmentNatureModel> GetPunishmentNature(int id);
        Task<PunishmentNatureModel> SavePunishmentNature(int id, PunishmentNatureModel model);
        Task<bool> DeletePunishmentNature(int id);
        Task<List<SelectModel>> GetPunishmentNatureSelectModels();

    }
}