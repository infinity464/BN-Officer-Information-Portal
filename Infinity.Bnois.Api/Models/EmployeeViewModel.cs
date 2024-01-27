using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class EmployeeViewModel
    {
        public EmployeeModel Employee { get; set; }
        public List<TabModel> TabItems { get; set; }
        public List<SelectModel> Batches { get; set; }
        public List<SelectModel> ExecutionRemarks { get; set; }
        public List<SelectModel> Genders { get; set; }
        public List<SelectModel> RankCategories { get; set; }
        public List<SelectModel> OfficerTypes { get; set; }
        public List<SelectModel> Ranks { get; set; }
        public List<SelectModel> Countries { get; set; }
        public List<SelectModel> EmployeeStatuses { get; set; }

    }
}
