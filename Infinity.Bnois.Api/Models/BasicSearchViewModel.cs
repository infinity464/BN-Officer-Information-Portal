using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class BasicSearchViewModel
    {
        
        public List<SelectModel> ColumnFilters { get; set; }
        public List<SelectModel> ColumnDisplays { get; set; }
        public List<SelectModel> Areas { get; set; }
        public List<SelectModel> AdminAuthorities { get; set; }
        public List<SelectModel> Branches { get; set; }
        public List<SelectModel> SubBranches { get; set; }
        public List<SelectModel> Exams { get; set; }
        public List<SelectModel> Results { get; set; }
        public List<SelectModel> Institutes { get; set; }
        public List<SelectModel> CommissionTypes { get; set; }
        public List<SelectModel> Ranks { get; set; }
        public List<SelectModel> Countries { get; set; }
        public List<SelectModel> ServiceExamCategories { get; set; }
        public List<SelectModel> CourseCategories { get; set; }
        public List<SelectModel> CourseSubCategories { get; set; }
        public List<SelectModel> TrainingInstitutes { get; set; }
        public List<SelectModel> Courses { get; set; }
        public List<SelectModel> PassingYears { get; set; }
     
    }
}
