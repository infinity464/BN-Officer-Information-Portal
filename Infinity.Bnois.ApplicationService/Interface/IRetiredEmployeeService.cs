using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IRetiredEmployeeService
    {
        List<EmployeeModel> GetRetiredEmployees(int ps, int pn, string qs, out int total);
        Task<RetiredEmployeeModel> GetRetiredEmployee(int employeeId);
        Task<RetiredEmployeeModel> SaveRetiredEmployee(int v, RetiredEmployeeModel model);
      
       
    }
}
