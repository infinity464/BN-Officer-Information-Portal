using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class MissionAppointmentViewModel
    {
        public MissionAppointmentModel MissionAppointment { get; set; }
        public List<SelectModel> AptNats { get; set; }
        public List<SelectModel> AptCats { get; set; }
        public List<SelectModel> RankList { get; set; }
        public List<SelectModel> BranchList { get; set; }
        public List<SelectModel> Missions { get; set; }
    }
}