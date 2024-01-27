using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeCourseFuturePlanService
    {
        List<EmployeeCourseFuturePlanModel> GetEmployeeCourseFuturePlans(string pNo);
        Task<EmployeeCourseFuturePlanModel> GetEmployeeCourseFuturePlan(int id);
        Task<EmployeeCourseFuturePlanModel> SaveEmployeeCourseFuturePlan(int v, EmployeeCourseFuturePlanModel model);
        Task<bool> DeleteEmployeeCourseFuturePlan(int id);
      
      

    }
}
