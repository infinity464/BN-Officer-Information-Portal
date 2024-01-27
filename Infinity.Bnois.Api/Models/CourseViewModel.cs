using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class CourseViewModel
    {
        public CourseModel Course { get; set; }
        public List<SelectModel> CourseCategories { get; set; }
        public List<SelectModel> CourseSubCategories { get; set; }
        public List<SelectModel> Countries { get; set; }
    }
}