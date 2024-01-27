using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IMedalService
    {
        List<MedalModel> GetMedals(int ps, int pn, string qs, out int total);
        Task<MedalModel> SaveMedal(int id, MedalModel model);
        Task<MedalModel> GetMedal(int id);
        Task<bool> DeleteMedal(int id);
        Task <List<SelectModel>> GetMedalSelectModels(int medalType);
        Task <List<SelectModel>> GetMedalSelectModels();
	    List<SelectModel> GetMedalTypeSelectModels();
        Task<List<SelectModel>> GetTraceMedalSelectModels(int medalType);
    }
}
