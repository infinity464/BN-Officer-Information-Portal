using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBonusPtPublicService
    {
        List<BonusPtPublicModel> GetBonusPtPublics(int id);
        Task<BonusPtPublicModel> GetBonusPtPublic(int bonusPtPublicId);
        Task<BonusPtPublicModel> SaveBonusPtPublic(int v, BonusPtPublicModel model);
    }
}
