using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class TrainingPlanViewModel
    {
        public TrainingPlanModel TrainingPlan { get; set; }
        public List<SelectModel> CourseCategories { get; set; }
        public List<SelectModel> CourseSubCategories { get; set; }
        public List<SelectModel> Courses { get; set; }
        public List<SelectModel> Countries { get; set; }
        public List<SelectModel> CountryTypes { get; set; }
        public List<SelectModel> TrainingInstitutes { get; set; }
        public List<SelectModel> RankList { get; set; }
        public List<SelectModel> BranchList { get; set; }
    }
}