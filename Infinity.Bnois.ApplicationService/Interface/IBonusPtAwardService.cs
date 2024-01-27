using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
   public interface IBonusPtAwardService
    {
        List<BonusPtAwardModel> GetBonusPtAwards(int id);
        Task<BonusPtAwardModel> GetBonusPtAward(int bonusPtAwardId);
        Task<BonusPtAwardModel> SaveBonusPtAward(int v, BonusPtAwardModel model);
    }
}
