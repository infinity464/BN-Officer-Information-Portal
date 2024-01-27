using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISpecialAptTypeService
    {

        List<SpecialAptTypeModel> GetSpecialAptTypes(int ps, int pn, string qs, out int total);
        Task<SpecialAptTypeModel> GetSpecialAptType(int id);
        Task<SpecialAptTypeModel> SaveSpecialAptType(int v, SpecialAptTypeModel model);
        Task<bool> DeleteSpecialAptType(int id);

        Task<List<SelectModel>> GetSpecialAptTypeSelectModels();
    }
}