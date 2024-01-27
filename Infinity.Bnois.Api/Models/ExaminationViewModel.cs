using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
   public class ExaminationViewModel
    {
        public List<SelectModel> ExamCategories { get; set; }
        public ExaminationModel Examination { get; set; }
    }
}
