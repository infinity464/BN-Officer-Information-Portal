using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface INominationDetailService
    {
        List<NominationDetailModel> GetNominationDetails(int id);
        Task<NominationDetailModel> GetNominationDetail(int id);
        Task<List<SelectModel>> GetNominatedList(int nominationId);
        Task<NominationDetailModel> SaveNominationDetail(int id, int type, NominationDetailModel model);
        Task<List<NominationDetailModel>> UpdateNominationDetails(int id, List<NominationDetailModel> model);
        Task<bool> DeleteNominationDetail(int id);
    
    }
}
