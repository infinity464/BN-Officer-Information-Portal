using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IStatusChangeService
    {
        List<StatusChangeModel> GetStatusChanges(int ps, int pn, string qs, out int total);
        Task<StatusChangeModel> GetStatusChange(int id);
        Task<StatusChangeModel> SaveStatusChange(int v, StatusChangeModel model);
        Task<bool> DeleteStatusChange(int id);
        Task<int> GetEmployeeCommissionType(int employeeId);

        Task<int> GetEmployeeBranch(int employeeId);
        Task<int> GetEmployeeMedicalCategory(int employeeId);
        Task<int> GetEmployeeEyeVision(int employeeId);


    }
}
