using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class EmployeeServiceExamResultViewModel
    {
        public EmployeeServiceExamResultModel EmployeeServiceExamResult { get; set; }
        public List<SelectModel> ServiceExams { get; set; }
        public List<SelectModel> ServiceExamCategories { get; set; }
    }
}