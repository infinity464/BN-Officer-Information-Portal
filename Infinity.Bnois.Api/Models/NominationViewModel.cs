using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class NominationViewModel
    {
        public NominationModel Nomination { get; set; }
        public List<SelectModel> NominationTypes { get; set; }
        public List<SelectModel> NominationTypeResults { get; set; }
        public List<SelectModel> MissionAppointments { get; set; }

    }
}
