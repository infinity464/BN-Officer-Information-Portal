using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPtDeductPunishmentService
    {
        List<PtDeductPunishmentModel> GetPtDeductPunishments(int id);
        Task<PtDeductPunishmentModel> GetPtDeductPunishment(int ptDeductPunishmentId);
        Task<PtDeductPunishmentModel> SavePtDeductPunishment(int v, PtDeductPunishmentModel model);
    }
}
