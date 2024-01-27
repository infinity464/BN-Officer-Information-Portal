using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPreCommissionRankService
    {
        List<PreCommissionRankModel> GetPreCommissionRanks(int ps, int pn, string qs, out int total);
        Task<PreCommissionRankModel> GetPreCommissionRank(int id);
        Task<PreCommissionRankModel> SavePreCommissionRank(int v, PreCommissionRankModel model);
        Task<bool> DeletePreCommissionRank(int id);
        Task<List<SelectModel>> GetPreCommissionRankSelectModels();
    }
}
