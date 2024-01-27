using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
   public interface IDistrictService
    {
        List<DistrictModel> GetDistricts(int ps, int pn, string qs, out int total);
        Task<DistrictModel> GetDistrict(int id);
        Task<DistrictModel> SaveDistrict(int v, DistrictModel model);
        Task<bool> DeleteDistrict(int id);
        Task<List<SelectModel>> GetDistrictSelectModels();
        Task<List<SelectModel>> GetDistrictByDivisionSelectModels(int divisionId);
        Task<List<SelectModel>> GetDistrictsSelectModelByDivion(int divisionId);
    }
}
