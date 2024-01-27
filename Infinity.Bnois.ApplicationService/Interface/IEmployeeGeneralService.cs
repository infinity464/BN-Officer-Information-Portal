using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeGeneralService
    {
        Task<EmployeeGeneralModel> GetEmployeeGenerals(int employeeId);
        Task<EmployeeGeneralModel> SaveEmployeeGeneral(int v, EmployeeGeneralModel model);
        Task<EmployeeGeneralModel> GetEmployeeGeneralByPNo(string pno);

        bool UpdateEmployeeEmailContact(string PNo, string EmailAddress, string ContactNo);
    }
}
