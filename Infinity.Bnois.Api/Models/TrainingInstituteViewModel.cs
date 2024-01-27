using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class TrainingInstituteViewModel
    {
        public TrainingInstituteModel TrainingInstitute { get; set; }
        public List<SelectModel> CountryTypes { get; set; }
        public List<SelectModel> Countries { get; set; }

    }
}