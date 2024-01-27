using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ITerminationTypeService
    {

        List<TerminationTypeModel> GetTerminationTypes(int ps, int pn, string qs, out int total);
        Task<TerminationTypeModel> GetTerminationType(int id);
        Task<TerminationTypeModel> SaveTerminationType(int v, TerminationTypeModel model);
        Task<bool> DeleteTerminationType(int id);
        Task<List<SelectModel>> GetTerminationTypeSelectModels();


    }
}