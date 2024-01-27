using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IPreviousLeaveService
    {
        Task<PreviousLeaveModel> SavePreviousLeave(int previousLeaveId, PreviousLeaveModel model);
        Task<PreviousLeaveModel> GetPreviousLeave(int previousLeaveId);
        List<PreviousLeaveModel> GetPreviousLeaves(int employeeId);
        Task<bool> DeletePreviousLeave(int id);
    }
}
