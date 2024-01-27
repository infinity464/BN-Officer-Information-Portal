using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeServiceExamResultService
    {
        List<EmployeeServiceExamResultModel> GetEmployeeServiceExamResults(int ps, int pn, string qs, out int total);
        Task<EmployeeServiceExamResultModel> GetEmployeeServiceExamResult(int id);
        Task<EmployeeServiceExamResultModel> SaveEmployeeServiceExamResult(int v, EmployeeServiceExamResultModel model);
        Task<bool> DeleteEmployeeServiceExamResult(int id);


    }
}
