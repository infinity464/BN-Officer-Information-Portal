using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;

namespace Infinity.Bnois.Api.Models
{
   public class ExamSubjectViewModel
    {
        public ExamSubjectModel ExamSubject { get; set; }
        public List<SelectModel>  ExamCategories { get; set; }
        public List<SelectModel> Examinations { get; set; }
        
    }
}
