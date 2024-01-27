using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IMedalAwardService
    {
        List<MedalAwardModel> GetMedalAwards(int ps, int pn, string qs, out int total);
        Task<MedalAwardModel> GetMedalAward(int id);
        Task<MedalAwardModel> SaveMedalAward(int id, MedalAwardModel model);
        Task<bool> DeleteMedalAward(int id);
        List<SelectModel> GetMedalAwardTypeSelectModels();
 
    }
}
