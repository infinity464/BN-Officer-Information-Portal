using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
  public  class ResultModel
    {
        public int ResultId { get; set; }
        public int ExamCategoryId { get; set; }
        public string ResultCode { get; set; }
        public string Name { get; set; }
        public double MinGPA { get; set; }
        public double MaxGPA { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ExamCategoryModel ExamCategory { get; set; }
    }
}
