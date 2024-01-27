using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBonusPtComAppService
    {
        List<BonusPtComAppModel> GetBonusPtComApps(int id);
        Task<BonusPtComAppModel> GetBonusPtComApp(int bonusPtComAppId);
        Task<BonusPtComAppModel> SaveBonusPtComApp(int v, BonusPtComAppModel model);
    }
}
