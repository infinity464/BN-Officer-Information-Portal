using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeSportService
    {
        Task<EmployeeSportModel> SaveEmployeeSport(int employeeId, EmployeeSportModel model);
        Task<EmployeeSportModel> GetEmployeeSport(int employeeSportId);
        List<EmployeeSportModel> GetEmployeeSports(int employeeId);
        Task<bool> DeleteEmployeeSport(int id);
    }
}
