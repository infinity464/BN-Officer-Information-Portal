using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeHajjDetaitService
    {
        List<EmployeeHajjDetailModel> GetEmployeeHajjDetails(int ps, int pn, string qs, out int total);
        List<EmployeeHajjDetailModel> GetEmployeeHajjDetailsByPno(string PNo);
        Task<EmployeeHajjDetailModel> getEmployeeHajjDetail(int EmployeeHajjDetailId);
        Task<EmployeeHajjDetailModel> SaveEmployeeHajjDetail(int v, EmployeeHajjDetailModel model);
        Task<bool> DeleteEmployeeHajjDetail(int id);


    }
}
