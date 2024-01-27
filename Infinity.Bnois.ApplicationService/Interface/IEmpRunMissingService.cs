using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEmpRunMissingService
    {
        List<EmpRunMissingModel> GetEmpRunMissings(int ps, int pn, string qs, out int total);
        Task<EmpRunMissingModel> GetEmpRunMissing(int id);
        Task<EmpRunMissingModel> SaveEmpRunMissing(int id, EmpRunMissingModel model);
        Task<bool> DeleteEmpRunMissing(int id);

        List<SelectModel> GetStatusTypeSelectModels();
        List<EmpRunMissingModel> GetBackToUnits(int ps, int pn, string qs, out int total);
        Task<EmpRunMissingModel> SaveEmpBackToUnit(int id, EmpRunMissingModel model);
    }
}