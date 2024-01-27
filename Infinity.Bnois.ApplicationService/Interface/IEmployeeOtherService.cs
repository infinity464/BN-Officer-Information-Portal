using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeOtherService
    {
        Task<EmployeeOtherModel> GetEmployeeOthers(int employeeId);
        Task<EmployeeOtherModel> SaveEmployeeOther(int employeeId, EmployeeOtherModel model);
        Task<EmployeeOtherModel> UpdateEmployeeOther(EmployeeOtherModel model);
    }
}
