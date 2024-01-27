using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeService
    {
        List<EmployeeModel> GetEmployees(int ps, int pn, string qs, out int total);
        Task<EmployeeModel> SaveEmployee(int v, EmployeeModel model);
        Task<bool> DeleteEmployee(int id);
        Task<EmployeeModel> GetEmployee(int id);
        Task<EmployeeModel> GetEmployeeByPO(string id);
        Task<List<SelectModel>> GetOfficerTypeSelectModels();
        Task<List<SelectModel>> GetEmployeeStatusSelectModels();
        List<EmployeeModel> GetEmployeesByDollarSign(int ps, int pn, string qs, out int total);
        Task<EmployeeModel> UpdateEmployeeDollarSign(EmployeeModel model);
        Task<bool> DeleteEmployeeDollarSign(int employeeId);
        Task<bool> ExecuteSeniorityProcess();
        Task<string> UpdateAgeServicePolicy();

        bool ExecuteNamingConvention();
        List<object> GetEmployeeByName(string qs);
        string GetLoginEmployeePno();
        Task<EmployeeModel> GetEmployeeByPO();
    }
}
