using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeSecurityClearanceService
    {

        List<EmployeeSecurityClearanceModel> GetEmployeeSecurityClearances(int ps, int pn, string qs, out int total);
        Task<EmployeeSecurityClearanceModel> GetEmployeeSecurityClearance(int id);
        Task<EmployeeSecurityClearanceModel> SaveEmployeeSecurityClearance(int v, EmployeeSecurityClearanceModel model);
        Task<bool> DeleteEmployeeSecurityClearance(int id);


    }
}