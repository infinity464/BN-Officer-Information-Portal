using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeChildrenService
    {
        List<EmployeeChildrenModel> GetEmployeeChildrens(int employeeId);
        Task<EmployeeChildrenModel> GetEmployeeChildren(int employeeChildrenId);
        Task<EmployeeChildrenModel> SaveEmployeeChildren(int v, EmployeeChildrenModel model);
        List<SelectModel> GetChildrenTypeSelectModels();

        Task<bool> DeleteEmployeeChildren(int id);
        Task<EmployeeChildrenModel> UpdateEmployeeChildren(EmployeeChildrenModel employeeChildren);
    }
}
