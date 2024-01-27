using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IUpazilaService
    {
        List<UpazilaModel> GetUpazilas(int ps, int pn, string qs, out int total);
        Task<UpazilaModel> GetUpazila(int id);
        Task<UpazilaModel> SaveUpazila(int v, UpazilaModel model);
        Task<bool> DeleteUpazila(int id);
        Task<List<SelectModel>> GetUpazilaSelectModels();
        Task<List<SelectModel>> GetUpazilaByDistrictSelectModels(int districtId);
        Task<List<SelectModel>> GetUpazilasSelectModelByDistrict(int districtId);
    }
}