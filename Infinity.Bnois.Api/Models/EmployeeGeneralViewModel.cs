using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class EmployeeGeneralViewModel
    {
        public EmployeeGeneralModel EmployeeGeneral { get; set; }
        public EmployeeModel Employee { get; set; }
        public List<SelectModel> Categories { get; set; }
        public List<SelectModel> ExecutionRemarks { get; set; }
        public List<SelectModel> SubCategories { get; set; }
        public List<SelectModel> CommissionTypes { get; set; }
        public List<SelectModel> Branches { get; set; }
        public List<SelectModel> SubBranches { get; set; }
        public List<SelectModel> Subjects { get; set; }
        public List<SelectModel> Nationalities { get; set; }
        public List<SelectModel> MaritalTypes { get; set; }
        public List<SelectModel> Religions { get; set; }
        public List<SelectModel> ReligionCasts { get; set; }
        public List<SelectModel> OfficerStreams { get; set; }
    }
}
