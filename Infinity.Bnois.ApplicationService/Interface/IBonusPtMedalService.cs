using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBonusPtMedalService
    {
        List<BonusPtMedalModel> GetBonusPtMedals(int id);
        Task<BonusPtMedalModel> GetBonusPtMedal(int bonusPtMedalId);
        Task<BonusPtMedalModel> SaveBonusPtMedal(int v, BonusPtMedalModel model);
    }
}
