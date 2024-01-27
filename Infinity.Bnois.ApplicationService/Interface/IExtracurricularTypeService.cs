using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IExtracurricularTypeService
    {
        Task<ExtracurricularTypeModel> GetExtracurricularType(int id);
        List<ExtracurricularTypeModel> GetExtracurricularTypes(int ps, int pn, string qs, out int total);
        Task<ExtracurricularTypeModel> SaveExtracurricularType(int v, ExtracurricularTypeModel model);
        Task<bool> DeleteExtracurricularType(int id);
        Task<List<SelectModel>> GetExtracurricularTypeSelectModels();
    }
}
