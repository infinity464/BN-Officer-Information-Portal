using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeTransferFuturePlanService
    {
        List<EmployeeTransferFuturePlanModel> GetEmployeeTransferFuturePlans(string pno);
        Task<EmployeeTransferFuturePlanModel> GetEmployeeTransferFuturePlan(int id);
        Task<EmployeeTransferFuturePlanModel> SaveEmployeeTransferFuturePlan(int v, EmployeeTransferFuturePlanModel model);
        Task<bool> DeleteEmployeeTransferFuturePlan(int id);
      
      

    }
}
