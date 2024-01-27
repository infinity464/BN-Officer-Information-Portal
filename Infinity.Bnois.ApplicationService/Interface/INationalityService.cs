using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface INationalityService
    {
        List<NationalityModel> GetNationalities(int ps, int pn, string qs, out int total);
        Task<NationalityModel> GetNationality(int id);
        Task<NationalityModel> SaveNationality(int v, NationalityModel model);
        Task<bool> DeleteNationality(int id);
        Task<List<SelectModel>> GetNationalitySelectModels();
      
    }
}
