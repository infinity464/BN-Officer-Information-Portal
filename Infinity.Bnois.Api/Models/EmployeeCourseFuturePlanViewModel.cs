using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class EmployeeCourseFuturePlanViewModel
    {
        public EmployeeCourseFuturePlanModel CourseFuturePlan { get; set; }
        public List<SelectModel> Courses { get; set; }
        public List<SelectModel> CourseCategories { get; set; }
        public List<SelectModel> CourseSubCategories { get; set; }
    }
}