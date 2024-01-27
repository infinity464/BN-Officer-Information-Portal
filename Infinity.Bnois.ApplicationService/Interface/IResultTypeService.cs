using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IResultTypeService
    {
        List<ResultTypeModel> GetResultTypes(int ps, int pn, string qs, out int total);
        Task<ResultTypeModel> GetResultType(int id);
        Task<ResultTypeModel> SaveResultType(int v, ResultTypeModel model);
        Task<bool> DeleteResultType(int id);
        Task<List<SelectModel>> GetResultTypeSelectModels();
    }
}