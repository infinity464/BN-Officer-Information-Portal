using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IDivisionService
    {
        List<DivisionModel> GetDivisions(int ps, int pn, string qs, out int total);
        Task<DivisionModel> GetDivision(int id);
        Task<DivisionModel> SaveDivision(int v, DivisionModel model);
        Task<bool> DeleteDivision(int id);
        Task<List<SelectModel>> GetDivisionSelectModels();
    }
}