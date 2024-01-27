using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface INominationService
    {
        IQueryable<vwNomination> GetNominations(int ps, int pn, string qs, out int total,int type);
        Task<NominationModel> GetNomination(int id);
        string GetNominationSchedule(int id, int type);
        Task<NominationModel> SaveNomination(int id, NominationModel model);
        Task<bool> DeleteNomination(int id);
        List<SelectModel> GetNominationTypeSelectModels();
        Task<List<SelectModel>> GetNominationSelectModels(int type);
        
    }
}
