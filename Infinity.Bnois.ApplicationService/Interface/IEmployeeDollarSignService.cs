using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeDollarSignService
    {
        List<EmployeeDollarSignModel> GetEmployeeDollarSigns(int ps, int pn, string qs, out int total);
        Task<EmployeeDollarSignModel> GetEmployeeDollarSign(int employeeDollarSignId);
        Task<EmployeeDollarSignModel> SaveEmployeeDollarSign(int v, EmployeeDollarSignModel model);
        Task<bool> DeleteEmployeeDollarSign(int employeeDollarSignId);
    }
}
