using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface INominationScheduleService
    {
        List<NominationScheduleModel> GetNominationSchedules(int ps, int pn, string qs,int type, out int total);
        Task<NominationScheduleModel> GetNominationSchedule(int id);
        Task<NominationScheduleModel> SaveNominationSchedule(int v, NominationScheduleModel model);
        Task<bool> DeleteNominationSchedule(int id);
        Task<List<SelectModel>> GetMissionNominationScheduleSelectModels();
        Task<List<SelectModel>> GetForeignVisitNominationScheduleSelectModels();
        Task<List<SelectModel>> GetOtherNominationScheduleSelectModels();
	    List<SelectModel> GetNominationScheduleTypeSelectModels();
       

    }
}
