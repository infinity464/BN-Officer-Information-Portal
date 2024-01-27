using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IHeirTypeService
    {
        List<HeirTypeModel> GetHeirTypes(int ps, int pn, string qs, out int total);
        Task<HeirTypeModel> GetHeirType(int id);
        Task<HeirTypeModel> SaveHeirType(int v, HeirTypeModel model);
        Task<bool> DeleteHeirType(int id);
        Task<List<SelectModel>> GetHeirTypeSelectModels();
    }
}
