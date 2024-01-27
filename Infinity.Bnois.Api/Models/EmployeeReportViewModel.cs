using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
   public class EmployeeReportViewModel
    {
        public EmployeeReportModel EmployeeReport { get; set; }
        public dynamic EmployeeReports { get; set; }
        public List<SelectModel> PassingYears { get; set; }
        public List<SelectModel> CourseCategories { get; set; }
       
    }
}
