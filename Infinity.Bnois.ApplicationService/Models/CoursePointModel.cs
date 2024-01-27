using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class CoursePointModel
    {
        public int CoursePointId { get; set; }
        public int TraceSettingId { get; set; }
        public int CourseCategoryId { get; set; }
        public Nullable<double> Max { get; set; }
        public Nullable<double> Min { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual CourseCategoryModel CourseCategory { get; set; }
        public virtual TraceSettingModel TraceSetting { get; set; }
    }
}
