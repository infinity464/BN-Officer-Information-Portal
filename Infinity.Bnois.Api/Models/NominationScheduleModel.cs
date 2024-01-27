using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.Api.Models
{
    public class NominationScheduleViewModel
    {
        public NominationScheduleModel NominationSchedule { get; set; }
        public List<SelectModel> Countries { get; set; }
        public List<SelectModel> VisitCategories { get; set; }
        public List<SelectModel> VisitSubCategories { get; set; }
    }
}