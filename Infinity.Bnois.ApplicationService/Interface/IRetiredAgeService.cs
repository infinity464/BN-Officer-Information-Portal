using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IRetiredAgeService
    {

        List<RetiredAgeModel> GetRetiredAges(int ps, int pn, string qs, out int total);
        Task<RetiredAgeModel> GetRetiredAge(int id);
        Task<RetiredAgeModel> SaveRetiredAge(int v, RetiredAgeModel model);
        Task<bool> DeleteRetiredAge(int id);
        List<SelectModel> GetRStatusSelectModels();

    }
}