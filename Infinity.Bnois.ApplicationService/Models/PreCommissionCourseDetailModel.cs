using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class PreCommissionCourseDetailModel
    {
        public int PreCommissionCourseDetailId { get; set; }
        public long PreCommissionCourseId { get; set; }
        public string ModuleName { get; set; }
        public string OutOfMark { get; set; }
        public string AchievedMark { get; set; }
        public string Position { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual PreCommissionCourseModel PreCommissionCourse { get; set; }
    }
}
