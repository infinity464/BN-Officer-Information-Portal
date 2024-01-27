using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeOprService
    {
        List<EmployeeOprModel> GetEmployeeOprs(int ps, int pn, string qs, out int total);
        Task<EmployeeOprModel> GetEmployeeOpr(int id);
        Task<EmployeeOprModel> SaveEmployeeOpr(int v, EmployeeOprModel model);
        Task<bool> DeleteEmployeeOpr(int id);
        Task<EmployeeOprModel> UpdateEmployeeOpr(EmployeeOprModel model);
    }
}
