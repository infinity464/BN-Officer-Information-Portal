using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.Api.Models
{
    public class OPRAppointmentViewModel
    {
      
        public List<SelectModel> SpecialAppointmentTypes { get; set; }
        public List<SelectModel> Suitabilities { get; set; }
    }
}