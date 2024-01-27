using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class TrainingResultViewModel
    {
        public TrainingResultModel TrainingResult { get; set; }
        public List<SelectModel> ResultTypes { get; set; }
        public List<SelectModel> TrainingPlans { get; set; }
        public List<SelectModel> ResultStatus { get; set; }
        public List<SelectModel> CourseCategories { get; set; }
        public List<SelectModel> CourseSubCategories { get; set; }
        public List<SelectModel> Countries { get; set; }
        public FileModel File { get; set; }
    }
}