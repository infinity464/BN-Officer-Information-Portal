using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
   public class PreviousExperienceViewModel
    {
        public PreviousExperienceModel PreviousExperience { get; set; }
        public List<SelectModel> Categories { get; set; }
        public List<SelectModel> PreCommissionRanks { get; set; }
       
    }
}
