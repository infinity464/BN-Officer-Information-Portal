using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IRankMapService
    {
        List<RankMapModel> GetRankMaps(int ps, int pn, string qs, out int total);
        Task<RankMapModel> GetRankMap(int id);
        Task<RankMapModel> SaveRankMap(int v, RankMapModel model);
        Task<bool> DeleteRankMap(int id);
    }
}
