using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;

namespace Infinity.Bnois.Api.Models
{
   public class EducationViewModel
    {
        public EducationModel Education { get; set; }
        public ExamCategoryModel ExamCategory { get; set; }
        public FileModel File { get; set; }
        public List<SelectModel> ExamCategories { get; set; }
        public List<SelectModel> Examinations { get; set; }
        public List<SelectModel> Boards { get; set; }
        public List<SelectModel> Institutes { get; set; }
        public List<SelectModel> Subjects { get; set; }
        public List<SelectModel> Results { get;  set; }
        public List<SelectModel> Years { get; set; }
        public List<SelectModel> CourseDurations { get; set; }
        public List<SelectModel> Grades { get; set; }
    }
}
