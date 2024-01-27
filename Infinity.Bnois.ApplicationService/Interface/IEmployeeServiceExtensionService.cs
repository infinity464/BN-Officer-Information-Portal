using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmployeeServiceExtensionService
    {
        List<EmployeeServiceExtensionModel> GetEmployeeServiceExtensions(int ps, int pn, string qs, out int total);
        Task<EmployeeServiceExtensionModel> GetEmployeeServiceExtension(int id);
        Task<EmployeeServiceExtensionModel> SaveEmployeeServiceExtension(int v, EmployeeServiceExtensionModel model);
        Task<bool> DeleteEmployeeServiceExtension(int id);
  
    }
}