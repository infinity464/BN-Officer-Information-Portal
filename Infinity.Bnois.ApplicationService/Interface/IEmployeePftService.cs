using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeePftService
    {
        List<EmployeePftModel> GetEmployeePfts(int ps, int pn, string qs, out int total);
        Task<EmployeePftModel> GetEmployeePft(int id);
        Task<EmployeePftModel> SaveEmployeePft(int v, EmployeePftModel model);
        Task<bool> DeleteEmployeePft(int id);


    }
}
