using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPreviousMissionService
    {
        Task<PreviousMissionModel> SavePreviousMission(int previousMissionId, PreviousMissionModel model);
        Task<PreviousMissionModel> GetPreviousMission(int previousMissionId);
        List<PreviousMissionModel> GetPreviousMissions(int employeeId);
        Task<bool> DeletePreviousMission(int id);
    }
}
