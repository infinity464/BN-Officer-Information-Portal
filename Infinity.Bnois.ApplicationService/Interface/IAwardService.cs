using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IAwardService
    {
        List<AwardModel> GetAwards(int ps, int pn, string qs, out int total);
        Task<AwardModel> SaveAward(int id, AwardModel model);
        Task<AwardModel> GetAward(int id);
        Task<bool> DeleteAward(int id);
        Task <List<SelectModel>> GetAwardSelectModels();

    }
}
