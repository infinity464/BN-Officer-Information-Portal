using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISecurityClearanceReasonService
    {
        List<SecurityClearanceReasonModel> GetSecurityClearanceReasons(int ps, int pn, string qs, out int total);
        Task<SecurityClearanceReasonModel> GetSecurityClearanceReason(int id);
        Task<SecurityClearanceReasonModel> SaveSecurityClearanceReason(int v, SecurityClearanceReasonModel model);
        Task<bool> DeleteSecurityClearanceReason(int id);
        Task<List<SelectModel>> GetSecurityClearanceReasonSelectModels();
    }
}