using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class ExtraAppointmentViewModel
    {
        public ExtraAppointmentModel ExtraAppointment { get; set; }
        public List<SelectModel> Offices { get; set; }
        public List<SelectModel> Appointments { get; set; }
    }
}