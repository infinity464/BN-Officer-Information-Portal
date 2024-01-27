using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPreviousPunishmentService
    {
        Task<PreviousPunishmentModel> SavePreviousPunishment(int previousPunishmentId, PreviousPunishmentModel model);
        Task<PreviousPunishmentModel> GetPreviousPunishment(int previousPunishmentId);
        List<PreviousPunishmentModel> GetPreviousPunishments(int employeeId);
        Task<bool> DeletePreviousPunishment(int id);
    }
}
