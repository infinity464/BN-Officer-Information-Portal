using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISpouseService
    {
        List<SpouseModel> GetSpouses(int employeeId);
        Task<SpouseModel> GetSpouse(int spouseId);
        Task<SpouseModel> SaveSpouse(int spouseId, SpouseModel model);
        List<SelectModel> GetCurrentStatusSelectModels();
        List<SelectModel> GetRelationTypeSelectModels();
        Task<List<SelectModel>> GetSpouseSelectModels(int employeeId);

        Task<bool> DeleteSpouse(int id);
        Task<SpouseModel> UpdateSpouse(SpouseModel spouse);
    }
}